using AI;
using ScoreBehaviour;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace ScoreUI
{
    public sealed class ScoreViewModel : MonoBehaviour
    {
        private ReactiveProperty<string> _score = new ReactiveProperty<string>();
        private ScoreService<EnemyType> _scoreService;
        private IDisposable _handle;

        private const string FormatScore = "Score: ";

        public ReactiveProperty<string> Score => _score;

        [Inject]
        public void Constructor(ScoreService<EnemyType> scoreService)
        {
            _scoreService = scoreService;
            SubscribeUpdateScoreText();
        }

        private void OnDestroy()
        {
            _handle?.Dispose();
        }

        private void SubscribeUpdateScoreText()
        {
            _handle = _scoreService.Score.Subscribe(value =>
            {
                _score.Value = GetFormatScore(value);
            });
        }

        private string GetFormatScore(int score)
        {
            return FormatScore + score.ToString();
        }
    }
}
