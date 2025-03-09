using ObjectPoolService;
using UnityEngine;

namespace FactoryService
{
    [System.Serializable]
    public class FactoryObjData<T> where T : Object
    {
        public string Key;
        public T Obj;
        public ObjectPool<T> ObjectPool;
    }
}
