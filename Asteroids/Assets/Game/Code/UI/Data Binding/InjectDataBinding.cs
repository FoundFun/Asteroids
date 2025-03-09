using Zenject;

namespace DataBinding
{
    public sealed class InjectDataBinding<T>
    {
        private InjectDataBindingSettings<T> _injectDataBindingSettings;
        private DiContainer _diContainer;

        public InjectDataBinding(InjectDataBindingSettings<T> injectDataBindingSettings, DiContainer diContainer)
        {
            _injectDataBindingSettings = injectDataBindingSettings;
            _diContainer = diContainer;
        }

        public T GetModel()
        {
            switch (_injectDataBindingSettings.InjectType)
            {
                case InjectType.Instance:
                    return _injectDataBindingSettings.Instance;
                case InjectType.Resolve:
                    return _diContainer.Resolve<T>();
                case InjectType.ResolveFromID:
                    return _diContainer.ResolveId<T>(_injectDataBindingSettings.Id);
                default:
                    return _injectDataBindingSettings.Instance;
            }
        }
    }
}
