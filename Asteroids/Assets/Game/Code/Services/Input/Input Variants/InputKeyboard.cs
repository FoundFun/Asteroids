using UnityEngine;
using Zenject;

namespace InputBehaviour
{
    public sealed class InputKeyboard : IInput
    {
        private SignalBus _signalBus;

        public SignalBus SignalBus { get => _signalBus; }

        [Inject]
        public InputKeyboard(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void UpdateInput()
        {
            OnMovement();
            OnClick();
            OnSuperClick();
        }

        private void OnMovement()
        {
            Vector2 directionInput = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
                directionInput += Vector2.up;
            if (Input.GetKey(KeyCode.S))
                directionInput += Vector2.down;
            if (Input.GetKey(KeyCode.A))
                directionInput += Vector2.left;
            if (Input.GetKey(KeyCode.D))
                directionInput += Vector2.right;

            directionInput = directionInput.normalized;
            _signalBus.Fire(new InputMovementSignal(directionInput));
        }

        private void OnClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _signalBus.Fire(new InputClickSignal(true, Input.mousePosition));
            }
        }

        private void OnSuperClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _signalBus.Fire(new InputSuperClickSignal(true, Input.mousePosition));
            }
        }
    }
}
