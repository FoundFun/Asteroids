using FactoryService;
using UnityEngine;
using Utilities;
using Zenject;

namespace AI
{
    public sealed class EnemyFactory : FactoryBase<BaseEnemy>
    {
        private AsyncMethod _asyncMethod;
        private Transform _parent;

        private const string ParentName = "Enemy Factory";

        [Inject]
        public EnemyFactory(FactoryData<BaseEnemy> data, DiContainer diContainer) : base(data, diContainer)
        {

        }

        ~EnemyFactory()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        public override void OnInstantiateObj(BaseEnemy obj)
        {
            base.OnInstantiateObj(obj);
            SetParent(obj);
            JsonEnemyData data = DiContainer.ResolveId<JsonEnemyData>(obj.GetEnemyType.ToString());
            obj.SetData(data);
        }

        public override void OnCreateObj(BaseEnemy obj)
        {
            base.OnCreateObj(obj);
            StartReturnToPool(obj);
        }

        private void StartReturnToPool(BaseEnemy obj)
        {
            _asyncMethod = new AsyncMethod(() => obj.IsDead || obj.IsOutStageZone, null, null, () =>
            {
                if (obj != null)
                    ReturnToPool(obj, obj.GetEnemyType.ToString());
            });

            _asyncMethod.Run();
        }

        private void SetParent(BaseEnemy obj)
        {
            if (_parent == null)
            {
                _parent = new GameObject(ParentName).transform;
            }

            obj.transform.SetParent(_parent);
        }
    }
}
