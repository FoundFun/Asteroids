using FxService;
using HealthSystem;
using ObjectPoolService;
using PhysicsBehaviour;
using ScoreBehaviour;
using System;
using Unit;
using UnityEngine;
using Utilities;
using Utilities.Static;
using Zenject;

namespace AI
{
    public class BaseEnemy : MonoBehaviour, ITakeDamage, IObjectPoolObject, IDisposable, IUnit
    {
        [Header("Base Enemy")]
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _maxHealth = 1;
        [SerializeField] private bool _scaleShow = true;
        [SerializeField] private float _timeScaleShow = 1f;
        [SerializeField] private string _deadFxKey;
        [SerializeField] protected PhysicsUnit _physicsUnit;

        private bool _isOutStageZone;
        private float _timer = 0f;
        private Vector3 _startScale = Vector3.one * 0.01f;
        private Vector3 _endScale = Vector3.one;
        private AsyncMethod _asyncMethod;
        private Health _health;
        private AIBehaviour _aiBehaviour;
        private FxFactory _fxFactory;
        private ScoreService<EnemyType> _scoreService;
        protected EnemyFactory _enemyFactory;

        public EnemyType GetEnemyType => _enemyType;
        public bool IsOutStageZone => _isOutStageZone;
        public bool IsDead => _health.IsDead;
        public Health Health => _health;
        public PhysicsUnit PhysicsUnit => _physicsUnit;

        [Inject]
        public void Constructor(FxFactory fxFactory, EnemyFactory enemyFactory, ScoreService<EnemyType> scoreService)
        {
            _fxFactory = fxFactory;
            _enemyFactory = enemyFactory;
            _scoreService = scoreService;
            Init();
        }

        protected virtual void OnEnable()
        {
            _health.onDead += OnDead;

            if (_scaleShow)
                PlayScaleShowAnimation();
        }

        private void Update()
        {
            _aiBehaviour.Update();
        }

        protected virtual void OnDisable()
        {
            _health.onDead -= OnDead;

            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (gameObject.activeSelf == false) return;
            if (collision.gameObject.layer != Layers.StageZone) return;

            TakeDamage(_health.MaxHealth);
            _isOutStageZone = true;
        }

        public void Dispose()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        public void SetData(JsonEnemyData data)
        {
            _maxHealth = data.health;
            _physicsUnit.PhysicsSettings.maxSpeed = data.maxSpeed;
            _physicsUnit.PhysicsSettings.acceleration = data.accelerationSpeed;
            _physicsUnit.PhysicsSettings.rotationSpeed = data.speedRotation;
            _physicsUnit.PhysicsSettings.rotationAcceleration = data.accelerationRotation;

            Init();
        }

        public virtual void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        public void OnReset()
        {
            _isOutStageZone = false;
            _health = new Health(_maxHealth);
        }

        protected virtual void Init()
        {
            InitHealth();
            InitAIBehaviour();
        }

        protected virtual void OnDead()
        {
            if (_deadFxKey != string.Empty)
                _fxFactory.Create(_deadFxKey, transform.position);

            _scoreService.Add(_enemyType);
        }

        protected virtual void SetState(IAIState state)
        {
            _aiBehaviour.ChangeState(state);
        }

        protected virtual IAIState GetCurrentState()
        {
            return _aiBehaviour.CurrentState;
        }

        protected void SetDirection(Vector2 direction)
        {
            _physicsUnit.SetDirection(direction);
        }

        private void InitHealth()
        {
            _health = new Health(_maxHealth);
        }

        private void InitAIBehaviour()
        {
            _aiBehaviour = new AIBehaviour();
        }

        private void PlayScaleShowAnimation()
        {
            if (_asyncMethod != null)
            {
                _asyncMethod.Stop();
            }

            _asyncMethod = new AsyncMethod(() =>
            {
                _timer = 0f;
                transform.localScale = _startScale;
            }, () =>
            {
                _timer += Time.deltaTime;
                float lerp = _timer / _timeScaleShow;

                if (lerp < 1f)
                {
                    transform.localScale = Vector3.Lerp(_startScale, _endScale, lerp);
                }
                else
                {
                    transform.localScale = _endScale;
                }
            }, null);

            _asyncMethod.Run();
        }
    }
}