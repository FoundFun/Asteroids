using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ScoreBehaviour
{
    public sealed class ScoreService<T>
    {
        private ReactiveProperty<int> _score = new ReactiveProperty<int>();
        private Dictionary<T, int> _scoreRewards = new Dictionary<T, int>();

        public ReactiveProperty<int> Score => _score;

        public ScoreService(Dictionary<T, int> scoreRewards)
        {
            _scoreRewards = scoreRewards;
        }

        public void Add(T rewardKey)
        {
            int addScore;

            if (_scoreRewards.TryGetValue(rewardKey, out addScore))
            {
                _score.Value += addScore;
            }
            else
            {
                Debug.LogWarning("Not find reward key: " + rewardKey);
            }
        }

        public void Reset()
        {
            _score.Value = 0;
        }
    }
}
