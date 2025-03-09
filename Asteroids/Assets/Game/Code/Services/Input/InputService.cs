using Zenject;

namespace InputBehaviour
{
    public sealed class InputService : ITickable
    {
        private IInput _input;

        [Inject]
        public InputService(IInput input)
        {
            _input = input;
        }

        public void Tick()
        {
            UpdateInputs();
        }

        private void UpdateInputs()
        {
            if (_input == null) return;

            _input.UpdateInput();
        }
    }
}
