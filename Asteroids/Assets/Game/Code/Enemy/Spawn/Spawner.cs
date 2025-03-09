using StageBehaviour;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;
using Zenject;

namespace AI
{
    public sealed class Spawner : IInitializable, ILateDisposable
    {
        private int _maxSpawnCount;
        private int _delaySpawn;
        private EnemyFactory _enemyFactory;
        private Stage _stage;
        private Transform _parent;
        private AsyncMethod _asyncMethod;
        private List<BaseEnemy> _enemies = new List<BaseEnemy>();

        private const string ParentName = "Spawner";

        [Inject]
        public Spawner(JsonStageData jsonStageData, EnemyFactory enemyFactory, StageGenerator stageGenerator)
        {
            _maxSpawnCount = jsonStageData.enemyMaxCount;
            _delaySpawn = jsonStageData.enemyDelaySpawn;
            _enemyFactory = enemyFactory;
            _stage = stageGenerator.Stage;
        }

        ~Spawner()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        public void Initialize()
        {
            StartSpawning();
        }

        public void LateDispose()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        private void SpawnEnemy(EnemyType type, Vector2 position)
        {
            BaseEnemy enemy = _enemyFactory.Create(type.ToString(), position);
            enemy.transform.parent = _parent;

            if (_enemies.Contains(enemy) == false)
                _enemies.Add(enemy);
        }

        private void StartSpawning()
        {
            _parent = new GameObject().transform;
            _parent.gameObject.name = ParentName;

            _asyncMethod = new AsyncMethod(_delaySpawn, false, null, null, () =>
            {
                if (GetSpawnCount() < _maxSpawnCount)
                {
                    Vector2 positionOnBorder = _stage.GetRandomPointOnBorder();
                    Vector2 direction = (positionOnBorder - _stage.GetCenterStage()).normalized;
                    SpawnEnemy(Random.Range(0, 2) == 0 ? EnemyType.Asteroid : EnemyType.EnemyShip, positionOnBorder + direction);
                }
            });

            _asyncMethod.Run();
        }

        private int GetSpawnCount()
        {
            return _enemies.Where(x => x != null && x.gameObject.activeSelf).Count();
        }
    }
}