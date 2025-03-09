using UnityEngine;

namespace InputBehaviour
{
    public sealed class InputSuperClickSignal
    {
        private bool _isSuperClick;
        private Vector2 _position;

        public bool IsSuperClick => _isSuperClick;
        public Vector2 Position => _position;

        public InputSuperClickSignal(bool isSuperClick, Vector2 position)
        {
            _isSuperClick = isSuperClick;
        }
    }
}
