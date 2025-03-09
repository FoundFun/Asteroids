using Ads;
using AdsAdapterIronSource;
using Analytics;
using AnalyticsAdapterFirebase;
using InputBehaviour;
using PhysicsBehaviour;
using SceneBehaviour;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] GameObject _adsOnPausePrefab;

        public override void InstallBindings()
        {
            ApplyFrameRate();
            BindSceneLoader();
            BindInputKeyboard();
            BindInputJoystick();
            BindInputService();
            BindPhysicsMovementCalculations();
            BindAnalytics();
            BindAds();
        }

        private void ApplyFrameRate()
        {
            Application.targetFrameRate = 60;
        }

        private void BindSceneLoader()
        {
            Container.Bind<SceneLoader>()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<InputService>()
                .AsSingle();
        }

        private void BindInputKeyboard()
        {
            if (InputDevice.GetDeviceType() != DeviceType.Desktop) return;

            Container.Bind<IInput>()
                .To<InputKeyboard>()
                .AsSingle();
        }

        private void BindInputJoystick()
        {
            if (InputDevice.GetDeviceType() != DeviceType.Handheld) return;

            Container.Bind<IInput>()
                .To<InputJoystick>()
                .AsSingle();
        }

        private void BindPhysicsMovementCalculations()
        {
            Container.Bind<PhysicsMovementCalculations>()
                .AsSingle();
        }

        private void BindAds()
        {
            Container.Bind<AdsAdapter>()
                 .To<IronSourceAdapter>()
                 .AsSingle();

            Container.Bind<AdsService>()
                .AsSingle();

            Container.InstantiatePrefab(_adsOnPausePrefab);
        }

        private void BindAnalytics()
        {
            Container.Bind<AnalyticsAdapter>()
                 .To<FirebaseAdapter>()
                 .AsSingle();

            Container.Bind<AnalyticsService>()
                .AsSingle();
        }
    }
}
