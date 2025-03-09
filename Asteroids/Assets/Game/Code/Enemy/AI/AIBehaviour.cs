namespace AI
{
    public sealed class AIBehaviour
    {
        private IAIState _currentState;

        public IAIState CurrentState => _currentState;

        public void Update()
        {
            _currentState?.Update();
        }

        public void ChangeState(IAIState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}