using BaseUI;
using System;
using UniRx;

namespace LaserUI
{
    public sealed class LaserViewModel : UIElement
    {
        public ReactiveProperty<string> LaserCount = new ReactiveProperty<string>("0");
        public ReactiveProperty<float> PercentReloadCount = new ReactiveProperty<float>(1f);

        private LaserView _laserView;
        private ShootingBehaviour.Shooting _shooting;
        private IDisposable _handleLaserCount;
        private IDisposable _handleReload;

        private void OnDestroy()
        {
            _handleLaserCount?.Dispose();
            _handleReload?.Dispose();
        }

        internal void SetView(LaserView laserView)
        {
            _laserView = laserView;
        }

        internal void SetModel(ShootingBehaviour.IShooting shooting)
        {
            _shooting = shooting.Shooting;
            SubscribeLaserCount();
            SubscribeLaserReload();
        }

        private void SubscribeLaserCount()
        {
            _handleLaserCount = _shooting.CurrentCharge.Subscribe(value =>
            {
                LaserCount.Value = value.ToString();
            });
        }

        private void SubscribeLaserReload()
        {
            _handleReload = _shooting.PercentReload.Subscribe(value =>
            {
                PercentReloadCount.Value = value;
                _laserView.SetFillImage(value);
            });
        }
    }
}
