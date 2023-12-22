using System;
using ARShooter.Interface;
using UnityEngine;

namespace ARShooter
{
    public class Shootable : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health m_Health;
        [SerializeField] private ParticleSystem m_ImpactParticle;
        public event Action OnDamageReceived;
        
        public void OnDamage()
        {
            OnDamageReceived?.Invoke();
            m_Health.SetHealth(-20);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag($"Projectile")) return;
            // OnDamage();
            ShowImpact(other.ClosestPoint(transform.position));
            other.gameObject.SetActive(false);
        }

        public void ShowImpact(Vector3 position)
        {
            if (!m_ImpactParticle) return;
            m_ImpactParticle.transform.position = position;
            m_ImpactParticle?.Play();
        }
    }
}