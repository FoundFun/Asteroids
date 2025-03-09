using UnityEngine;
using Utilities.Static;

namespace HealthSystem
{
    public sealed class DoDamageOnTrigger : MonoBehaviour
    {
        [Header("Do Damage On Trigger")]
        [SerializeField] private int _damage;
        [SerializeField] private int _maxCountDoDamage = -1;
        [SerializeField] private float _delay = 0.2f;
        [SerializeField] private LayerMask _mask;

        private int _currentCountDoDamage;
        private float _timer;

        public bool IsMaxCountDoDamage => _maxCountDoDamage != -1 && _currentCountDoDamage >= _maxCountDoDamage;

        private void OnEnable()
        {
            _timer = _delay;
        }

        private void Update()
        {
            if (_timer < _delay)
            {
                _timer += Time.deltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsMaxCountDoDamage) return;
            if (_timer < _delay) return;
            if (MaskUtilities.IsLayerInMask(collision.gameObject.layer, _mask) == false) return;

            ITakeDamage iTakeDamage = collision.gameObject.GetComponent<ITakeDamage>();

            if (iTakeDamage == null) return;

            iTakeDamage.TakeDamage(_damage);
            _timer = 0f;
            _currentCountDoDamage++;
        }

        public void OnReset()
        {
            _currentCountDoDamage = 0;
            _timer = 0f;
        }
    }
}
