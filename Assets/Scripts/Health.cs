using System;
using UnityEngine;

namespace ARShooter
{
    public class Health : MonoBehaviour
    {
        public event Action OnHealthIsEmpty;
        [SerializeField] private int m_MaxHealth = 100; 
        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = m_MaxHealth;
        }

        public void SetHealth(int value)
        {
            _currentHealth += value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, m_MaxHealth);
            if (_currentHealth <= 0)
            {
                OnHealthIsEmpty?.Invoke();
            }
        }
    }
}