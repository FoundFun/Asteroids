using UnityEngine;
using Zenject;

namespace FactoryService
{
    public interface IFactory<T> where T : Object
    {
        public DiContainer DiContainer { get; }
        public FactoryData<T> FactoryData { get; }
        public T Instantiate(string key);
    }
}