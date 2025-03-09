using AI;
using BaseUI;
using DataBinding;
using FactoryService;
using FxService;
using InputBehaviour;
using ShootingBehaviour;
using ShootingBehaviour.Projectile;
using Unit;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class FactoriesInstaller : MonoInstaller
    {
        [Header("Fx Factory")]
        [SerializeField] private FxFactoryData _fxFactoryData;
        [Header("Projectile Factory")]
        [SerializeField] private ProjectileFactoryData _projectileFactoryData;
        [Header("Enemy Factory")]
        [SerializeField] private EnemyFactoryData _enemyFactoryData;
        [Header("UI Factory")]
        [SerializeField] private UIFactoryData _uiFactoryData;

        public override void InstallBindings()
        {
            BindFxFactory();
            BindProjectileFactory();
            BindEnemyFactory();
            BindDataBindingFactory();
            BindInjectDataBindingFactory();
            BindUIFactory();
        }

        private void BindFxFactory()
        {
            Container.Bind<FactoryData<ParticleSystem>>()   
                .FromInstance(_fxFactoryData)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<FxFactory>()
                .AsSingle();
        }

        private void BindProjectileFactory()
        {
            Container.Bind<FactoryData<Projectile>>()
                .FromInstance(_projectileFactoryData)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ProjectileFactory>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.Bind<FactoryData<BaseEnemy>>()
                .FromInstance(_enemyFactoryData)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyFactory>()
                .AsSingle();
        }

        private void BindDataBindingFactory()
        {
            Container.Bind<DataBindingFactory>()
                .AsSingle();
        }

        private void BindInjectDataBindingFactory()
        {
            Container.Bind<InjectDataBindingFactory<IUnit>>()
                .AsSingle();

            Container.Bind<InjectDataBindingFactory<IShooting>>()
                .AsSingle();

            Container.Bind<InjectDataBindingFactory<IInput>>()
                .AsSingle();
        }

        private void BindUIFactory()
        {
            Container.Bind<UIFactoryData>()
                .FromInstance(_uiFactoryData)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UIFactory>()
                .AsSingle();
        }
    }
}
