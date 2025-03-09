using DataBinding;
using UnityEngine;
using Zenject;

namespace ScoreUI
{
    public sealed class ScoreDataBinding : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ScoreViewModel _scoreViewModel;

        [Inject]
        public void Constructor(DataBindingFactory dataBindingFactory)
        {
            dataBindingFactory.CreateTextBinder(_scoreViewModel.Score, _scoreView.ScoreText);
        }
    }
}
