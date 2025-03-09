using UniRx;
using UnityEngine;
using Zenject;

namespace InputBehaviour
{
    public interface IInput
    {
        public SignalBus SignalBus { get; }
        public void UpdateInput();
    }
}
