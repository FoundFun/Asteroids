using UniRx;
using UnityEngine;

namespace HealthSystem
{
    public sealed class Health
    {
        private bool _isInvulnerability = false;
        private bool _isDead = false;
        private ReactiveProperty<int> _currentHealth = new ReactiveProperty<int>();
        private int _maxHealth;

        public ReactiveProperty<int> CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsDead => _isDead;
        public bool IsInvulnerability => _isInvulnerability;

        public event System.Action<int, int> onTakeDamage;
        public event System.Action<int, int> onHeal;
        public event System.Action onDead;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth.Value = _maxHealth;
            _isDead = false;
        }

        public void TakeDamage(int damage)
        {
            if (_isInvulnerability) return;
            if (_isDead) return;

            AddCurrentHealth(-damage);
            onTakeDamage?.Invoke(damage, _currentHealth.Value);

            if (_currentHealth.Value <= 0)
                Dead();
        }

        public void Heal(int heal)
        {
            if (_isDead) return;

            AddCurrentHealth(heal);
            onHeal?.Invoke(heal, _currentHealth.Value);
        }

        public void SetInvulnerability(bool enable)
        {
            _isInvulnerability = enable;
        }

        private void Dead()
        {
            if (_isDead) return;

            _isDead = true;
            onDead?.Invoke();
        }

        private void AddCurrentHealth(int value)
        {
            _currentHealth.Value += value;
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, 0, _maxHealth);
        }
    }
}
