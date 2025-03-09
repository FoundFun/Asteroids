using AI;
using PlayerBehaviour;
using StageBehaviour;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class JsonInstaller : MonoInstaller
    {
        [Header("Player")]
        [SerializeField] private bool _autoImportPlayerJson = true;
        [SerializeField] private PlayerJsonObject _playerJsonObject;
        [Header("Stage")]
        [SerializeField] private bool _autoImportStageJson = true;
        [SerializeField] private StageJsonObject _stageJsonObject;
        [Header("Enemies")]
        [SerializeField] private bool _autoImportEnemiesJson = true;
        [SerializeField] private List<EnemyJsonObject> _enemyJsonObjects = new List<EnemyJsonObject>();

        public override void InstallBindings()
        {
            BindStageJsonObject();
            BindPlayerJsonObject();
            BindEnemyJsonObjects();
        }

        private void BindStageJsonObject()
        {
            if (_autoImportStageJson)
                _stageJsonObject.Import();

            Container.Bind<StageJsonObject>()
                .FromInstance(_stageJsonObject)
                .AsSingle();

            Container.Bind<JsonStageData>()
                .FromInstance(_stageJsonObject.Data)
                .AsSingle();
        }

        private void BindPlayerJsonObject()
        {
            if (_autoImportPlayerJson)
                _playerJsonObject.Import();

            Container.Bind<PlayerJsonObject>()
                .FromInstance(_playerJsonObject)
                .AsSingle();

            Container.Bind<JsonPlayerData>()
                .FromInstance(_playerJsonObject.Data)
                .AsSingle();
        }

        private void BindEnemyJsonObjects()
        {
            foreach (var enemyJsonObject in _enemyJsonObjects)
            {
                if (_autoImportEnemiesJson)
                    enemyJsonObject.Import();

                Container.Bind<EnemyJsonObject>()
                    .WithId(enemyJsonObject.Data.EnemyType.ToString())
                    .FromInstance(enemyJsonObject);

                Container.Bind<JsonEnemyData>()
                    .WithId(enemyJsonObject.Data.EnemyType.ToString())
                    .FromInstance(enemyJsonObject.Data);
            }
        }
    }
}
