using InputBehaviour;
using Zenject;

namespace Installers
{
    public sealed class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            BindInputSignals();
        }

        private void BindInputSignals()
        {
            Container.DeclareSignal<InputMovementSignal>().OptionalSubscriber();
            Container.DeclareSignal<InputClickSignal>().OptionalSubscriber();
            Container.DeclareSignal<InputSuperClickSignal>().OptionalSubscriber();
        }
    }
}
