using DataBinding;
using Unit;
using UnityEngine;
using Zenject;

namespace UnitUI
{
    public sealed class UnitDataBinding : MonoBehaviour
    {
        [SerializeField] private InjectDataBindingSettings<IUnit> _inject;
        [SerializeField] private UnitView _unitView;
        [SerializeField] private UnitViewModel _unitViewModel;

        [Inject]
        public void Constructor(DataBindingFactory dataBindingFactory, InjectDataBindingFactory<IUnit> injectDataBindingFactory)
        {
            InjectDataBinding<IUnit> injectDataBinding = injectDataBindingFactory.CreateInjectDataBinding(_inject);
            _unitViewModel.SetView(_unitView);
            _unitViewModel.SetModel(injectDataBinding.GetModel());

            dataBindingFactory.CreateTextBinder(_unitViewModel.Position, _unitView.PositionText);
            dataBindingFactory.CreateTextBinder(_unitViewModel.Angle, _unitView.AngleText);
            dataBindingFactory.CreateTextBinder(_unitViewModel.Speed, _unitView.SpeedText);
        }
    }
}

