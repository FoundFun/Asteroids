using UnityEngine;

namespace InputBehaviour
{
    public sealed class InputClickSignal
    {
        private bool _isClick;
        private Vector2 _position;

        public bool IsClick => _isClick;
        public Vector2 Position => _position;

        public InputClickSignal(bool isClick, Vector2 position)
        {
            _isClick = isClick;
            _position = position;
        }
    }
}
