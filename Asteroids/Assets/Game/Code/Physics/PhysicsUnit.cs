using UnityEngine;
using Zenject;

namespace PhysicsBehaviour
{
    public sealed class PhysicsUnit : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private PhysicsSettings _physicsSettings;
        [SerializeField] private PhysicsUnitParameters _physicsUnitParameters;

        private Vector2 _direction;
        private PhysicsMovementCalculations _physicsMovementCalculations;

        public Rigidbody2D Rb => _rb;
        public PhysicsUnitParameters PhysicsUnitParameters => _physicsUnitParameters;
        public PhysicsSettings PhysicsSettings => _physicsSettings;

        public event System.Action onBounce;

        [Inject]
        public void Constructor(PhysicsMovementCalculations physicsMovementCalculations)
        {
            _physicsMovementCalculations = physicsMovementCalculations;
        }

        private void OnEnable()
        {
            _physicsMovementCalculations.onBounce += OnBounce;
        }

        private void FixedUpdate()
        {
            _physicsMovementCalculations.Move(_direction, _rb, _collider, ref _physicsUnitParameters, ref _physicsSettings);
        }

        private void OnDisable()
        {
            _physicsMovementCalculations.onBounce -= OnBounce;
        }

        public void SetIsTriggerCollider(bool enable)
        {
            _collider.isTrigger = enable;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void OnBounce(PhysicsSettings physicsSettings)
        {
            if (physicsSettings != _physicsSettings) return;

            onBounce?.Invoke();
        }
    }
}
