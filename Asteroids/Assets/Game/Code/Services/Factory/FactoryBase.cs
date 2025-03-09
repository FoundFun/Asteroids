using ObjectPoolService;
using UnityEngine;
using Zenject;

namespace FactoryService
{
    public class FactoryBase<T> : IFactory<T>, IInitializable where T : Object
    {
        private int _countInstance;
        protected int _lifeTime;
        private DiContainer _diContainer;
        private FactoryData<T> _data;

        public DiContainer DiContainer => _diContainer;
        public FactoryData<T> FactoryData => _data;

        [Inject]
        public FactoryBase(FactoryData<T> data, DiContainer diContainer)
        {
            _lifeTime = data.LifeTime;
            _countInstance = data.MaxCountInstance;
            _data = data;
            _diContainer = diContainer;

            foreach (var objData in _data.FactoryObjData)
            {
                objData.ObjectPool = new ObjectPool<T>(data.onInstantiate, data.onGet, data.onReturn);
            }
        }

        public void Initialize()
        {
            foreach (var objData in _data.FactoryObjData)
            {
                for (int i = 0; i < _countInstance; i++)
                {
                    objData.ObjectPool.InstantiateToPool(Instantiate(objData.Key));
                }
            }
        }

        public virtual void OnInstantiateObj(T obj)
        {

        }

        public virtual void OnCreateObj(T obj)
        {

        }

        public T Create(string key, Vector3 position)
        {
            T obj = _data.GetDataFromKey(key).ObjectPool.GetFromPool(position);
            OnCreateObj(obj);
            return obj;
        }

        public T Instantiate(string key)
        {
            T obj = _data.GetDataFromKey(key).Obj;

            if (obj == null)
            {
                Debug.LogWarning("Not find factory data prefab: " + key);
                return default;
            }

            T instance = _diContainer.InstantiatePrefabForComponent<T>(obj);
            OnInstantiateObj(instance);
            return instance;
        }

        protected void ReturnToPool(T obj, string key)
        {
            FactoryData.GetDataFromKey(key).ObjectPool.ReturnToPool(obj);
        }
    }
}
