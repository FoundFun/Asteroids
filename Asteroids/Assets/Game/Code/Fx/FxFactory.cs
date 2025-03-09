using FactoryService;
using UnityEngine;
using Utilities;
using Zenject;

namespace FxService
{
    public sealed class FxFactory : FactoryBase<ParticleSystem>
    {
        private AsyncMethod _asyncMethod;
        private Transform _parent;

        private const string ParentName = "Fx Factory";

        [Inject]
        public FxFactory(FactoryData<ParticleSystem> data, DiContainer diContainer) : base(data, diContainer)
        {
            _lifeTime = data.LifeTime;
        }

        ~FxFactory()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        public override void OnInstantiateObj(ParticleSystem obj)
        {
            base.OnInstantiateObj(obj);
            SetParent(obj);
        }

        public override void OnCreateObj(ParticleSystem obj)
        {
            base.OnCreateObj(obj);
            StartReturnToPool(obj);
        }

        private void StartReturnToPool(ParticleSystem obj)
        {
            _asyncMethod = new AsyncMethod(_lifeTime, true, null, null, () =>
            {
                if (obj != null)
                    ReturnToPool(obj, obj.name.Replace("(Clone)", string.Empty));
            });

            _asyncMethod.Run();
        }

        private void SetParent(ParticleSystem obj)
        {
            if (_parent == null)
            {
                _parent = new GameObject(ParentName).transform;
            }

            obj.transform.SetParent(_parent);
        }
    }
}
