using FactoryService;
using UnityEngine;
using Utilities;
using Zenject;

namespace ShootingBehaviour.Projectile
{
    public sealed class ProjectileFactory : FactoryBase<Projectile>
    {
        private AsyncMethod _asyncMethod;
        private Transform _parent;

        private const string ParentName = "Projectile Factory";

        [Inject]
        public ProjectileFactory(FactoryData<Projectile> data, DiContainer diContainer) : base(data, diContainer)
        {
            _lifeTime = data.LifeTime;
        }

        ~ProjectileFactory()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        public override void OnInstantiateObj(Projectile obj)
        {
            base.OnInstantiateObj(obj);
            SetParent(obj);
        }

        public override void OnCreateObj(Projectile obj)
        {
            base.OnCreateObj(obj);
            StartReturnToPool(obj);
        }

        private void StartReturnToPool(Projectile obj)
        {
            _asyncMethod = new AsyncMethod(_lifeTime, () => obj.IsMaxCountDoDamage, null, null, () =>
            {
                if (obj != null)
                    ReturnToPool(obj, obj.name.Replace("(Clone)", string.Empty));
            });

            _asyncMethod.Run();
        }

        private void SetParent(Projectile obj)
        {
            if (_parent == null)
            {
                _parent = new GameObject(ParentName).transform;
            }

            obj.transform.SetParent(_parent);
        }
    }
}
