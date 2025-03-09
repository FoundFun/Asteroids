using Ads;
using AI;
using Analytics;
using CameraBehaviour;
using FxService;
using HealthSystem;
using InputBehaviour;
using PhysicsBehaviour;
using SceneBehaviour;
using ScoreBehaviour;
using ShootingBehaviour;
using ShootingBehaviour.Projectile;
using StageBehaviour;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Utilities.Components;
using Utilities.Static;
using Zenject;

namespace PlayerBehaviour
{
    public sealed class Player : MonoBehaviour, ITarget, IUnit, IShooting
    {
        [Header("Settings")]
        [SerializeField] private float _delayInvulnerability = 3f;
        [SerializeField] private int _takeDamageOnBounce = 1;
        [SerializeField] private string _deadFxKey;
        [SerializeField] private string _takeDamageFxKey;
        [Header("Links")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _invulnerabilityFx;
        [SerializeField] private Transform _bulletPoint;
        [SerializeField] private PhysicsUnit _physicsUnit;
        [SerializeField] private SpriteRendererFade _spriteRendererFade;

        private Vector2 _direction;
        private Health _health;
        private CameraFollow _cameraFollow;
        private Shooting _bulletShooting;
        private Shooting _laserShooting;
        private FxFactory _fxFactory;
        private SceneLoader _sceneLoader;
        private Stage _stage;
        private InvulnerabilityBehaviour _invulnerabilityBehaviour;
        private SignalBus _signalBus;
        private AdsService _adsService;
        private AnalyticsService _analyticsService;
        private ScoreService<EnemyType> _scoreService;
        private Dictionary<string, System.Action> _shootingDictionary = new Dictionary<string, System.Action>();

        private const float DeltaDirection = 1f;

        public Transform Target => transform;
        public Health Health => _health;
        public PhysicsUnit PhysicsUnit => _physicsUnit;
        public Shooting Shooting => _laserShooting;
        public Dictionary<string, System.Action> ShootingDictionary => _shootingDictionary;

        [Inject]
        public void Constructor(CameraFollow cameraFollow, ProjectileFactory projectileFactory, FxFactory fxFactory, SceneLoader sceneLoader, StageGenerator stageGenerator, JsonPlayerData data, SignalBus signalBus, AdsService adsService, ScoreService<EnemyType> scoreService, AnalyticsService analyticsService)
        {
            _signalBus = signalBus;
            _adsService = adsService;
            _cameraFollow = cameraFollow;
            _analyticsService = analyticsService;
            _scoreService = scoreService;
            _cameraFollow.SetTarget(transform);
            _bulletShooting = new Shooting(projectileFactory, ProjectileType.Bullet, _bulletPoint, -1, 0, data.bulletSpeed);
            _shootingDictionary.Add(BindID.BulletShooting, () => OnShootBullet(null));
            _laserShooting = new Shooting(projectileFactory, ProjectileType.Laser, _bulletPoint, data.laserCharges, data.laserReloadTime);
            _shootingDictionary.Add(BindID.LaserShooting, () => OnShootLaser(null));
            _health = new Health(data.health);
            _fxFactory = fxFactory;
            _sceneLoader = sceneLoader;
            _stage = stageGenerator.Stage;
            _invulnerabilityBehaviour = new InvulnerabilityBehaviour(_health, _physicsUnit, _invulnerabilityFx, _spriteRenderer, _spriteRendererFade, _delayInvulnerability);
            ApplyPhysicsSettings(data);
            _signalBus.Subscribe<InputMovementSignal>(OnMovement);
            _signalBus.Subscribe<InputClickSignal>(OnShootBullet);
            _signalBus.Subscribe<InputSuperClickSignal>(OnShootLaser);
        }

        private void OnEnable()
        {
            _physicsUnit.onBounce += TakeDamage;
            _health.onDead += OnDead;
        }

        private void Update()
        {
            _bulletShooting.UpdateReload();
            _laserShooting.UpdateReload();
        }

        private void OnDisable()
        {
            _physicsUnit.onBounce -= TakeDamage;
            _health.onDead -= OnDead;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<InputMovementSignal>(OnMovement);
            _signalBus.Unsubscribe<InputClickSignal>(OnShootBullet);
            _signalBus.Unsubscribe<InputSuperClickSignal>(OnShootLaser);
            _invulnerabilityBehaviour.Dispose();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != Layers.StageBorder) return;

            transform.position = _stage.GetOppositePointOnBorder(transform.position);
        }

        private void TakeDamage()
        {
            if (_health.IsInvulnerability) return;

            if (_takeDamageFxKey != string.Empty)
                _fxFactory.Create(_takeDamageFxKey, transform.position);

            _health.TakeDamage(_takeDamageOnBounce);
        }

        private void OnMovement(InputMovementSignal inputMovementSignal)
        {
            if (_health.IsDead) return;

            _direction = Vector2.MoveTowards(_direction, inputMovementSignal.Direction, DeltaDirection);

            if (_health.IsInvulnerability)
                _direction = Vector2.zero;

            _physicsUnit.SetDirection(_direction);
        }

        private void OnShootBullet(InputClickSignal inputClickSignal)
        {
            if (inputClickSignal != null && inputClickSignal.IsClick == false) return;
            if (_health.IsInvulnerability) return;
            if (_health.IsDead) return;

            _bulletShooting.Shoot();
        }

        private void OnShootLaser(InputSuperClickSignal inputSuperClickSignal)
        {
            if (inputSuperClickSignal != null && inputSuperClickSignal.IsSuperClick == false) return;
            if (_health.IsInvulnerability) return;
            if (_health.IsDead) return;

            _laserShooting.Shoot(_bulletPoint);
        }

        private void OnDead()
        {
            if (_deadFxKey != string.Empty)
                _fxFactory.Create(_deadFxKey, transform.position);

            _analyticsService.SendScore(_scoreService.Score.Value);
            _adsService.ShowInterstitial();
            _sceneLoader.RestartScene(3);
            gameObject.SetActive(false);
        }

        private void ApplyPhysicsSettings(JsonPlayerData data)
        {
            _physicsUnit.PhysicsSettings.maxSpeed = data.maxSpeed;
            _physicsUnit.PhysicsSettings.acceleration = data.accelerationSpeed;
            _physicsUnit.PhysicsSettings.rotationSpeed = data.speedRotation;
            _physicsUnit.PhysicsSettings.rotationAcceleration = data.accelerationRotation;
        }
    }
}
