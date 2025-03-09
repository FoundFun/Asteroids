using HealthSystem;
using PhysicsBehaviour;
using System;
using UnityEngine;
using Utilities;

namespace AI
{
    public sealed class MoveToTargetState : IAIState, IDisposable
    {
        private int _delayUpdateDirection;
        private float _nearDistance;
        private bool _isStop;
        private ITarget _iTarget = null;
        private Vector3 _targetPosition;
        private PhysicsUnit _physicsUnit;
        private AsyncMethod _asyncMethod;

        public MoveToTargetState(PhysicsUnit physicsUnit, ITarget iTarget, int delayUpdateDirection, float nearDistance)
        {
            _physicsUnit = physicsUnit;
            _iTarget = iTarget;
            _delayUpdateDirection = delayUpdateDirection;
            _nearDistance = nearDistance;
        }

        ~MoveToTargetState()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();

            _iTarget = null;
        }

        public void Dispose()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();

            _iTarget = null;
        }

        public void Enter()
        {
            StartMovement();
            UpdateDirection();
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            _physicsUnit.SetDirection((_targetPosition - _physicsUnit.Rb.transform.position).normalized);

            if (IsNearTargetPosition() && _isStop == false)
            {
                UpdateDirection();
            }
        }
        
        private void StartMovement()
        {
            _asyncMethod = new AsyncMethod(_delayUpdateDirection, true, () =>
            {
                _isStop = true;
            }, null, () =>
            {
                if (_iTarget != null)
                {
                    if (_iTarget.Target != null)
                        SetTargetPosition(_iTarget.Target.position);
                }

                _isStop = false;
            });
        }

        private void SetTargetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        private bool IsNearTargetPosition()
        {
            return Vector3.Distance(_targetPosition, _physicsUnit.transform.position) <= _nearDistance;
        }

        private void UpdateDirection()
        {
            _asyncMethod.Run();
        }
    }
}
