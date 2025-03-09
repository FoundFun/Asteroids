using StageBehaviour;
using UnityEngine;
using Zenject;

namespace AI
{
    public sealed class AsteroidEnemy : BaseEnemy
    {
        [Header("Asteroid")]
        [SerializeField] private int _onDeadSpawnAsteroids = 3;

        private Stage _stage;

        [Inject]
        public void Constructor(StageGenerator stageGenerator)
        {
            _stage = stageGenerator.Stage;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetState(new MoveState(_physicsUnit, CalculateDirection()));
            _physicsUnit.onBounce += ChangeDirection;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _physicsUnit.onBounce += ChangeDirection;
        }

        protected override void OnDead()
        {
            base.OnDead();
            OnDeadSpawnAsteroids();
        }

        private void OnDeadSpawnAsteroids()
        {
            for (int i = 0; i < _onDeadSpawnAsteroids; i++)
            {
                _enemyFactory.Create(EnemyType.SmallAsteroid.ToString(), transform.position);
            }
        }

        private void ChangeDirection()
        {
            SetState(new MoveState(_physicsUnit, _physicsUnit.PhysicsUnitParameters.reflectionVector));
        }

        private Vector2 CalculateDirection()
        {
            Vector2 positionOnStage = _stage.GetRandomPointInStage();
            Vector2 direction = positionOnStage - new Vector2(_physicsUnit.Rb.transform.position.x, _physicsUnit.Rb.transform.position.y);
            return direction;
        }
    }
}
