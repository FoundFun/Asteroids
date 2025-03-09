using DataBinding;
using ShootingBehaviour;
using UnityEngine;
using Utilities.Static;
using Zenject;

namespace MobileInputUI
{
    public class MobileInputDataBinding : MonoBehaviour
    {
        [SerializeField] private InjectDataBindingSettings<IShooting> _inject;
        [SerializeField] private MobileInputView _view;
        [SerializeField] private MobileInputViewModel _viewModel;

        [Inject]
        public void Constructor(InjectDataBindingFactory<IShooting> injectDataBindingFactory, SignalBus signalBus, [Inject(Id = BindID.UICamera)] Camera uiCamera)
        {
            InjectDataBinding<IShooting> injectDataBinding = injectDataBindingFactory.CreateInjectDataBinding(_inject);
            _viewModel.SetView(_view);
            _viewModel.SetModel(injectDataBinding.GetModel(), signalBus, uiCamera);
        }
    }
}
