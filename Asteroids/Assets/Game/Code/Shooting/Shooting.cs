using ShootingBehaviour.Projectile;
using UniRx;
using UnityEngine;
using Utilities.Static;

namespace ShootingBehaviour
{
    public sealed class Shooting
    {
        private ReactiveProperty<int> _currentCharge = new ReactiveProperty<int>();
        private ReactiveProperty<int> _maxCharge = new ReactiveProperty<int>();
        private ReactiveProperty<float> _percentReload = new ReactiveProperty<float>(1f);
        private float _reloadTime;
        private float _timer;
        private float? _overrideSpeed = null;
        private ProjectileType _type;
        private ProjectileFactory _projectileFactory;
        private Transform _spawnPoint;

        public ReactiveProperty<int> CurrentCharge => _currentCharge;
        public ReactiveProperty<float> PercentReload => _percentReload;

        public Shooting(ProjectileFactory projectileFactory, ProjectileType type, Transform spawnPoint, int maxCharge = -1, float reloadTime = 0f, float? overrideSpeed = null)
        {
            _projectileFactory = projectileFactory;
            _type = type;
            _spawnPoint = spawnPoint;
            _maxCharge.Value = maxCharge;
            _reloadTime = reloadTime;
            _timer = 0f;
            _overrideSpeed = overrideSpeed;
        }

        public void Shoot(Transform overrideParent = null)
        {
            if (_maxCharge.Value != -1 && _currentCharge.Value == 0) return;

            if (_maxCharge.Value != -1)
                _currentCharge.Value--;

            Projectile.Projectile projectile = _projectileFactory.Create(_type.ToString(), _spawnPoint.position);
            projectile.transform.rotation = QuaternionUtilities.CalculateRotation(_spawnPoint);

            if (_overrideSpeed != null)
                projectile.SetSpeed(_overrideSpeed.Value);

            if (overrideParent != null)
            {
                projectile.transform.parent = overrideParent;
                projectile.transform.localPosition = Vector3.zero;
                projectile.transform.localEulerAngles = Vector3.zero;
            }

            projectile.Launch();
        }

        public void UpdateReload()
        {
            if (_maxCharge.Value == -1) return;
            if (_maxCharge.Value != -1 && _currentCharge.Value == _maxCharge.Value) return;

            _percentReload.Value = _timer / _reloadTime;

            if (_timer >= _reloadTime)
            {
                _timer = 0f;
                AddCharge();
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        private void AddCharge()
        {
            _currentCharge.Value++;
        }
    }
}
