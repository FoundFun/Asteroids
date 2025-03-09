using UnityEngine;

namespace InputBehaviour
{
    public sealed class InputMovementSignal
    {
        private Vector2 _direction;

        public Vector2 Direction => _direction;

        public InputMovementSignal(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
