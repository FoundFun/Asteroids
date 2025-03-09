using PhysicsBehaviour;
using UnityEngine;

namespace AI
{
    public sealed class MoveState : IAIState
    {
        private Vector2 _direction;
        private PhysicsUnit _physicsUnit;

        public MoveState(PhysicsUnit physicsUnit)
        {
            _physicsUnit = physicsUnit;
            SetDirection(Vector2.zero);
        }

        public MoveState(PhysicsUnit physicsUnit, Vector2 direction)
        {
            _physicsUnit = physicsUnit;
            SetDirection(direction);
        }

        public void Enter()
        {
            if (_direction == Vector2.zero)
                SetDirection(CalculateRandomDirection());
        }

        public void Exit()
        {

        }

        public void Update()
        {
            _physicsUnit.SetDirection(_direction);
        }

        private void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private Vector2 CalculateRandomDirection()
        {
            float angle = Random.Range(0f, 360f);
            float radians = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }
    }
}
