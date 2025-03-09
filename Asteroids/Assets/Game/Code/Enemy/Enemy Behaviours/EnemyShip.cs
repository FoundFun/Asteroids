using HealthSystem;
using UnityEngine;
using Utilities.Static;
using Zenject;

namespace AI
{
    public sealed class EnemyShip : BaseEnemy
    {
        [Header("Enemy Ship")]
        [SerializeField] private int _delayUpdateDirection = 3;
        [SerializeField] private float _nearDistance = 0.25f;

        private ITarget _iTarget;
        private MoveToTargetState _moveToTargetState;

        [Inject]
        public void Constructor([Inject(Id = BindID.Player)] ITarget iTarget)   
        {
            _iTarget = iTarget;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _moveToTargetState = new MoveToTargetState(_physicsUnit, _iTarget, _delayUpdateDirection, _nearDistance);
            SetState(_moveToTargetState);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_moveToTargetState != null)
                _moveToTargetState.Dispose();
        }

        private void OnDestroy()
        {
            if (_moveToTargetState != null)
                _moveToTargetState.Dispose();
        }
    }
}
