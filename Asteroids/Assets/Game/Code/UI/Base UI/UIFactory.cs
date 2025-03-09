using FactoryService;
using Utilities;
using Zenject;

namespace BaseUI
{
    public sealed class UIFactory : FactoryBase<UIElement>
    {
        private AsyncMethod _asyncMethod;

        [Inject]
        public UIFactory(UIFactoryData data, DiContainer diContainer) : base(data, diContainer)
        {

        }

        ~UIFactory()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        public override void OnInstantiateObj(UIElement obj)
        {
            base.OnInstantiateObj(obj);
        }

        public override void OnCreateObj(UIElement obj)
        {
            base.OnCreateObj(obj);
            obj.Show();
            StartReturnToPool(obj);
        }

        private void StartReturnToPool(UIElement obj)
        {
            _asyncMethod = new AsyncMethod(() => obj.IsShow == false, null, null, () =>
            {
                if (obj != null)
                    ReturnToPool(obj, obj.Key);
            });

            _asyncMethod.Run();
        }
    }
}
