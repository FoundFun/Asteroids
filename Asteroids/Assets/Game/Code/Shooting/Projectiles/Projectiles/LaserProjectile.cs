using UnityEngine;

namespace ShootingBehaviour.Projectile
{
    public sealed class LaserProjectile : IProjectile
    {
        private Transform _projectileObj;

        public LaserProjectile(Transform projectileObj)
        {
            _projectileObj = projectileObj;
        }

        public void Launch()
        {
            _projectileObj.localPosition = Vector3.zero;
            _projectileObj.localEulerAngles = Vector3.zero;
        }
    }
}
