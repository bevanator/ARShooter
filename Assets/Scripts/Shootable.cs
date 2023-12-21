using ARShooter.Interface;
using UnityEngine;

namespace ARShooter
{
    public class Shootable : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health m_Health;
        [SerializeField] private ParticleSystem m_ImpactParticle;
        [SerializeField] private ParticleSystem m_DeathParticle;

        
        private void Start()
        {
            m_Health.OnHealthIsEmpty += delegate
            {
                // OnShootableDeath?.Invoke();
            };
        }
        public void OnDamage()
        {
            m_Health.SetHealth(-20);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag($"Projectile")) return;
            OnDamage();
            ShowImpact(other.ClosestPoint(transform.position));
            other.gameObject.SetActive(false);
        }

        private void ShowImpact(Vector3 position)
        {
            if (!m_ImpactParticle) return;
            m_ImpactParticle.transform.position = position;
            m_ImpactParticle?.Play();
        }
    }
}