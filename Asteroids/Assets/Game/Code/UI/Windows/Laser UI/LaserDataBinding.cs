using DataBinding;
using ShootingBehaviour;
using UnityEngine;
using Zenject;

namespace LaserUI
{
    public sealed class LaserDataBinding : MonoBehaviour
    {
        [SerializeField] private InjectDataBindingSettings<IShooting> _inject;
        [SerializeField] private LaserView _laserView;
        [SerializeField] private LaserViewModel _laserViewModel;

        [Inject]
        public void Constructor(DataBindingFactory dataBindingFactory, InjectDataBindingFactory<IShooting> injectDataBindingFactory)
        {
            InjectDataBinding<IShooting> injectDataBinding = injectDataBindingFactory.CreateInjectDataBinding(_inject);
            _laserViewModel.SetView(_laserView);
            _laserViewModel.SetModel(injectDataBinding.GetModel());

            dataBindingFactory.CreateTextBinder(_laserViewModel.LaserCount, _laserView.CountLaserText);
        }
    }
}
