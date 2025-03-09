using HealthSystem;
using ObjectPoolService;
using PhysicsBehaviour;
using UnityEngine;

namespace ShootingBehaviour.Projectile
{
    public class Projectile : MonoBehaviour, IObjectPoolObject
    {
        [SerializeField] ProjectileType _type;
        [SerializeField] PhysicsUnit _physicsUnit;
        [SerializeField] DoDamageOnTrigger _damageOnTrigger;

        private IProjectile _iProjectile;

        public bool IsMaxCountDoDamage => _damageOnTrigger.IsMaxCountDoDamage;

        private void OnEnable()
        {
            Init();
        }

        public void Launch()
        {
            _iProjectile.Launch();
        }

        public void OnReset()
        {
            _damageOnTrigger.OnReset();
        }

        public void SetSpeed(float speed)
        {
            _physicsUnit.PhysicsSettings.maxSpeed = speed;
        }

        private void Init()
        {
            switch (_type)
            {
                case ProjectileType.Bullet:
                    _iProjectile = new BulletProjectile(_physicsUnit);
                    break;
                case ProjectileType.Laser:
                    _iProjectile = new LaserProjectile(transform);
                    break;
            }
        }
    }
}
