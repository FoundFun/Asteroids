using BaseUI;
using System;
using UniRx;
using Unit;

namespace UnitUI
{
    public sealed class UnitViewModel : UIElement
    {
        public ReactiveProperty<int> Health = new ReactiveProperty<int>();
        public ReactiveProperty<string> Position = new ReactiveProperty<string>();
        public ReactiveProperty<string> Angle = new ReactiveProperty<string>();
        public ReactiveProperty<string> Speed = new ReactiveProperty<string>();

        private bool _isInit;
        private UnitView _unitView;
        private IUnit _iUnit;
        private IDisposable _handleHealth;
        private IDisposable _handlePosition;
        private IDisposable _handleAngle;
        private IDisposable _handleSpeed;

        private void OnDestroy()
        {
            _handleHealth?.Dispose();
            _handlePosition?.Dispose();
            _handleAngle?.Dispose();
            _handleSpeed?.Dispose();
        }

        internal void SetView(UnitView unitView)
        {
            _unitView = unitView;
        }

        internal void SetModel(IUnit iUnit)
        {
            _iUnit = iUnit;
            SubscribeUpdateHealth();
            SubscribeUpdatePosition();
            SubscribeUpdateAngle();
            SubscribeUpdateSpeed();
        }

        private void SubscribeUpdateHealth()
        {
            _handleHealth = _iUnit.Health.CurrentHealth.Subscribe(value =>
            {
                Health.Value = value;
                InitHealthImages(value);
                UpdateHealthImages(value);
            });
        }

        private void SubscribeUpdatePosition()
        {
            _handlePosition = _iUnit.PhysicsUnit.PhysicsUnitParameters.currentPosition.Subscribe(value =>
            {
                Position.Value = "Position: " + value.ToString("F0");
            });
        }

        private void SubscribeUpdateAngle()
        {
            _handleAngle = _iUnit.PhysicsUnit.PhysicsUnitParameters.currentAngle.Subscribe(value =>
            {
                Angle.Value = "Angle: " + value.ToString("F0");
            });
        }

        private void SubscribeUpdateSpeed()
        {
            _handleSpeed = _iUnit.PhysicsUnit.PhysicsUnitParameters.currentVelocity.Subscribe(value =>
            {
                Speed.Value = "Speed: " + value.magnitude.ToString("F0");
            });
        }

        private void InitHealthImages(int health)
        {
            if (health == 0) return;
            if (_isInit) return;

            _isInit = true;

            if (_unitView.HealthImages.Count < health)
            {
                int countCreate = health - _unitView.HealthImages.Count;

                for (int i = 0; i < countCreate; i++)
                {
                    _unitView.CreateHealthImage();
                }
            }
        }

        private void UpdateHealthImages(int currentHealth)
        {
            int countEmpty = _unitView.HealthImages.Count - currentHealth;

            for (int i = 0; i < _unitView.HealthImages.Count; i++)
            {
                if (i < countEmpty)
                {
                    _unitView.SetHealthImageSprite(_unitView.HealthImages[i], true);
                }
                else
                {
                    _unitView.SetHealthImageSprite(_unitView.HealthImages[i], false);
                }
            }
        }
    }
}
