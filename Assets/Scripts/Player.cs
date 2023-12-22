using System;
using UnityEngine;

namespace ARShooter
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        private Health _health;
        public static event Action<float> OnHealthChanged;
        public static event Action OnPlayerDeath;
        private void Awake()
        {
            Instance = this;
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnHealthIsEmpty += RaisePlayerDeathEvent;
        }
        private void OnDisable()
        {
            _health.OnHealthIsEmpty -= RaisePlayerDeathEvent;
        }

        private void RaisePlayerDeathEvent()
        {
            OnPlayerDeath?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("Orb")) return;
            _health.SetHealth(-50);
            OnHealthChanged?.Invoke((float)_health.CurrentHealth/_health.MaxHealth);
            Destroy(other.gameObject);
        }
    }
}