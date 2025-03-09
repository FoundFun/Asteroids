using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FactoryService
{
    [System.Serializable]
    public class FactoryData<T> where T : Object
    {
        public int LifeTime;
        public int MaxCountInstance;
        public List<FactoryObjData<T>> FactoryObjData;

        public System.Action<T> onInstantiate;
        public System.Action<T, Vector3> onGet;
        public System.Action<T> onReturn;

        public FactoryObjData<T> GetDataFromKey(string key)
        {
            FactoryObjData<T> objData = FactoryObjData.FirstOrDefault(x => x.Key == key);

            if (objData == null)
            {
                Debug.LogWarning("Not find obj in factory data: " + key);
                return default;
            }
            else
            {
                return objData;
            }
        }
    }
}
