using PhysicsBehaviour;
using UnityEngine;
using Utilities;

namespace ShootingBehaviour.Projectile
{
    public sealed class BulletProjectile : IProjectile
    {
        private bool _isMoving = false;
        private PhysicsUnit _physicsUnit;
        private AsyncMethod _asyncMethod;

        public BulletProjectile(PhysicsUnit physicsUnit)
        {
            _physicsUnit = physicsUnit;

            _asyncMethod = new AsyncMethod(() =>
            {
                _isMoving = true;
            }, () =>
            {
                _physicsUnit.SetDirection(CalculateDirection());
            }, () =>
            {
                _isMoving = false;
            });
        }

        ~BulletProjectile()
        {
            Stop();
        }

        public void Launch()
        {
            if (_isMoving) return;

            _asyncMethod.Run();
        }

        public void Stop()
        {
            if (_isMoving == false) return;

            _asyncMethod.Stop();
        }

        private Vector3 CalculateDirection()
        {
            if (_physicsUnit == null || _physicsUnit.Rb == null)
            {
                return Vector3.zero;
            }

            Vector2 direction = _physicsUnit.Rb.transform.up;
            return direction;
        }
    }
}
