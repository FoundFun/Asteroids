using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolService
{
    public sealed class ObjectPool<T> where T : Object
    {
        private Queue<T> _inactiveObjects;
        private List<T> _activeObjects;

        private event System.Action<T> onInstantiate;
        private event System.Action<T, Vector3> onGet;
        private event System.Action<T> onReturn;

        public ObjectPool(System.Action<T> OnInstantiate, System.Action<T, Vector3> OnGet, System.Action<T> OnReturn)
        {
            _inactiveObjects = new Queue<T>();
            _activeObjects = new List<T>();
            onInstantiate = OnInstantiate;
            onGet = OnGet;
            onReturn = OnReturn;
        }

        public T GetFromPool(Vector3 position)
        {
            if (_inactiveObjects.Count == 0)
            {
                if (_activeObjects.Count == 0)
                {
                    Debug.LogError("Not find active object in pool.");
                    return default;
                }
                else
                {
                    _inactiveObjects.Enqueue(_activeObjects[0]);
                    Debug.LogError("Reset last object in pool");
                }
            }

            T obj = _inactiveObjects.Dequeue();
            _activeObjects.Add(obj);
            onGet?.Invoke(obj, position);
            return obj;
        }

        public void ReturnToPool(T obj)
        {
            if (_activeObjects.Contains(obj))
            {
                _activeObjects.Remove(obj);
                _inactiveObjects.Enqueue(obj);
                onReturn?.Invoke(obj);
                ResetObj(obj);
            }
            else
            {
                Debug.LogError("Not find object in pool");
            }
        }

        public void InstantiateToPool(T obj)
        {
            onInstantiate?.Invoke(obj);
            _inactiveObjects.Enqueue(obj);
        }

        private void ResetObj(T obj)
        {
            if (obj is IObjectPoolObject)
                (obj as IObjectPoolObject).OnReset();
        }
    }
}
