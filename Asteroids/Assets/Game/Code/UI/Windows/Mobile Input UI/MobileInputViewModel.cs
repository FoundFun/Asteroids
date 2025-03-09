using InputBehaviour;
using ShootingBehaviour;
using UniRx;
using UnityEngine;
using Utilities.Static;
using Zenject;

namespace MobileInputUI
{
    public class MobileInputViewModel : MonoBehaviour
    {
        [SerializeField] private float _multiplyJoystickFillDirection = 5f;
        [SerializeField] private float _lerpDirectionSpeed = 1f;
        [SerializeField] private float _lerpAlphaSpeed = 1f;

        public ReactiveProperty<bool> IsTouch = new ReactiveProperty<bool>(false);
        public ReactiveProperty<Vector2> TouchDirection = new ReactiveProperty<Vector2>(Vector2.zero);

        private Vector2 _currentDirection;
        private Vector2 _targetDirection;
        private float _currentAlpha;
        private float _targetAlpha;
        private MobileInputView _view;
        private IShooting _model;
        private Camera _uiCamera;

        private void Update()
        {
            UpdateLerpDirection();
            UpdateLerpAlpha();    
        }

        internal void SetView(MobileInputView view)
        {
            _view = view;
        }

        internal void SetModel(IShooting model, SignalBus signalBus, Camera uiCamera)
        {
            CheckDisable();
            _model = model;
            _uiCamera = uiCamera;
            SetBulletAction();
            SetLaserAction();
            signalBus.Subscribe<InputMovementSignal>(OnMovement);
            signalBus.Subscribe<InputClickSignal>(OnClick);
        }

        private void CheckDisable()
        {
            if (InputDevice.GetDeviceType() != DeviceType.Handheld)
            {
                gameObject.SetActive(false);
            }
        }

        private void SetBulletAction()
        {
            System.Action onBulletShooting;

            if (_model.ShootingDictionary.TryGetValue(BindID.BulletShooting, out onBulletShooting))
            {
                _view.SetActionBulletButton(() => onBulletShooting?.Invoke());
            }
        }

        private void SetLaserAction()
        {
            System.Action onLaserShooting;
            
            if (_model.ShootingDictionary.TryGetValue(BindID.LaserShooting, out onLaserShooting))
            {
                _view.SetActionLaserButton(() => onLaserShooting?.Invoke());
            }
        }

        private void OnMovement(InputMovementSignal inputMovementSignal)
        {
            IsTouch.Value = inputMovementSignal.Direction != Vector2.zero;
            TouchDirection.Value = inputMovementSignal.Direction;
            _targetAlpha = IsTouch.Value ? 1f : 0f;
            _targetDirection = TouchDirection.Value;
        }

        private void OnClick(InputClickSignal inputClickSignal)
        {
            _view.SetJoystickPosition(VectorUtilities.ScreenToWorldPoint(inputClickSignal.Position, _uiCamera));
        }

        private void UpdateLerpDirection()
        {
            _currentDirection = Vector2.MoveTowards(_currentDirection, _targetDirection, Time.deltaTime * _lerpDirectionSpeed);
            _view.SetJoystickFillDirection(_currentDirection, _multiplyJoystickFillDirection);
        }

        private void UpdateLerpAlpha()
        {
            _currentAlpha = Mathf.MoveTowards(_currentAlpha, _targetAlpha, Time.deltaTime * _lerpAlphaSpeed);
            _view.SetJoystickAlpha(_currentAlpha);
        }
    }
}
