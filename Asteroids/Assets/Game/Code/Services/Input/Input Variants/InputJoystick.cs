using UnityEngine;
using Utilities.Static;
using Zenject;

namespace InputBehaviour
{
    public sealed class InputJoystick : IInput
    {
        private SignalBus _signalBus;
        private Vector2 _cacheStartTouchPosition = Vector2.zero;
        private Vector2 _cacheLastTouchPosition = Vector2.zero;

        private const float MaxInputDistance = 100f;

        public SignalBus SignalBus => _signalBus;

        [Inject]
        public InputJoystick(SignalBus signalBus)
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
            if (Input.touchCount <= 0)
            {
                _signalBus.Fire(new InputMovementSignal(Vector2.zero));
                return;
            }

            Touch touch = Input.GetTouch(0);

            if (RaycastUtilities.IsTouchOverUI(touch))
            {
                _signalBus.Fire(new InputMovementSignal(Vector2.zero));
                return;
            }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _cacheStartTouchPosition = touch.position;
                    _cacheLastTouchPosition = Vector2.zero;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    Vector2 currentTouchPosition = touch.position;
                    Vector2 delta = currentTouchPosition - _cacheStartTouchPosition;
                    _cacheLastTouchPosition = new Vector2(Mathf.Clamp(delta.x / MaxInputDistance, -1f, 1f), Mathf.Clamp(delta.y / MaxInputDistance, -1f, 1f));
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _cacheStartTouchPosition = Vector2.zero;
                    _cacheLastTouchPosition = Vector2.zero;
                    break;
            }

            _signalBus.Fire(new InputMovementSignal(_cacheLastTouchPosition));
        }

        private void OnClick()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _signalBus.Fire(new InputClickSignal(false, touch.position));
                }
            }
        }

        private void OnSuperClick()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Stationary && touch.deltaTime > 1.0f)
                {
                    _signalBus.Fire(new InputSuperClickSignal(false, touch.position));
                }
            }
        }
    }
}
