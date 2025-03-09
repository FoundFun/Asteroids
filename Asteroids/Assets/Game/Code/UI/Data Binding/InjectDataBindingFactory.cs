using Zenject;

namespace DataBinding
{
    public sealed class InjectDataBindingFactory<T>
    {
        private DiContainer _diContainer;

        [Inject]
        public InjectDataBindingFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public InjectDataBinding<T> CreateInjectDataBinding(InjectDataBindingSettings<T> injectDataBindingSettings)
        {
            InjectDataBinding<T> injectDataBinding = new InjectDataBinding<T>(injectDataBindingSettings, _diContainer);
            return injectDataBinding;
        }
    }
}
