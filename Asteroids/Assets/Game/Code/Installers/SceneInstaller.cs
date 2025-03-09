using AI;
using CameraBehaviour;
using HealthSystem;
using PlayerBehaviour;
using ScoreBehaviour;
using ShootingBehaviour;
using StageBehaviour;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Utilities.Static;
using Zenject;

namespace Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [Header("Camera")]
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private Camera _uiCamera;
        [Header("Player")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _playerPrefab;

        private readonly Dictionary<EnemyType, int> _scoreRewardKeys = new Dictionary<EnemyType, int>() 
        { 
            { EnemyType.Asteroid, 2 },
            { EnemyType.SmallAsteroid, 1 },
            { EnemyType.EnemyShip, 3 }
        };

        public override void InstallBindings()
        {
            BindStageGenerator();
            BindScoreService();
            BindCameraFollow();
            BindUICamera();
            BindPlayer();
            BindSpawner();
        }

        private void BindCameraFollow()
        {
            CameraFollow cameraFollow = Container.InstantiatePrefabForComponent<CameraFollow>(_cameraFollow);

            Container.Bind<CameraFollow>()
                .FromInstance(cameraFollow)
                .AsSingle();
        }

        private void BindUICamera()
        {
            Container.Bind<Camera>()
                .WithId(BindID.UICamera)
                .FromInstance(_uiCamera)
                .AsSingle();
        }

        private void BindScoreService()
        {
            Container.Bind<ScoreService<EnemyType>>()
                .AsSingle()
                .WithArguments(_scoreRewardKeys);
        }

        private void BindStageGenerator()
        {
            Container.Bind<StageGenerator>()
                .AsSingle();
        }

        private void BindSpawner()
        {
            Container.BindInterfacesAndSelfTo<Spawner>()
                .AsSingle();
        }

        private void BindPlayer()
        {
            Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _spawnPoint.position, Quaternion.identity, _spawnPoint);

            Container.BindInterfacesAndSelfTo<Player>()
                .FromInstance(player)
                .AsSingle();

            Container.Bind<ITarget>()
                .WithId(BindID.Player)
                .FromInstance(player);

            Container.Bind<IUnit>()
                .WithId(BindID.Player)
                .FromInstance(player);

            Container.Bind<IShooting>()
                .WithId(BindID.Player)
                .FromInstance(player);
        }
    }
}
