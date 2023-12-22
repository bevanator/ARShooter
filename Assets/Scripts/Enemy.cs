using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ARShooter
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float m_MoveSpeed = 1f;
        private List<Vector3> _wayPoints = new();
        private int _currentIndex;

        private Health _health;
        private Shootable _shootable;
        private static readonly int Death = Animator.StringToHash("Death");

        [SerializeField] private bool m_Looping = true;
        [SerializeField] private int m_NumberOfPoints = 5;
        [SerializeField] private Animator m_Animator;
        [SerializeField] private ParticleSystem m_DeathParticle;
        private static readonly int Damage = Animator.StringToHash("Damage");

        private void Awake()
        {
            _shootable = GetComponent<Shootable>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnHealthIsEmpty += OnHealthIsEmpty;
            _shootable.OnDamageReceived += OnDamageReceived;
        }

        private void OnDisable()
        {
            _health.OnHealthIsEmpty -= OnHealthIsEmpty;
            _shootable.OnDamageReceived -= OnDamageReceived;
        }

        private void OnDamageReceived()
        {
            m_Animator.SetTrigger(Damage);
        }

        private void OnHealthIsEmpty()
        {
            m_Animator.SetTrigger(Death);
            m_DeathParticle.Play();
            _health.OnHealthIsEmpty -= OnHealthIsEmpty;
        }


        private void Start()
        {
            for (int i = 0; i < m_NumberOfPoints; i++)
            {
                Vector2 point = Random.insideUnitCircle * 5;
                _wayPoints.Add(new Vector3(point.x, 1.5f, point.y));
            }
            transform.LookAt(_wayPoints[0]);
        }

        private void Update()
        {
            MoveToTarget();
        }
        

        private void MoveToTarget()
        {
            if (!m_Looping && _currentIndex == m_NumberOfPoints - 1) return;
            float step = m_MoveSpeed * Time.deltaTime;
            Vector3 currentTarget = _wayPoints[_currentIndex];
            if (Vector3.Distance(transform.position, currentTarget) <= 0.1f)
            {
                if (_currentIndex == m_NumberOfPoints - 1)
                {
                    _currentIndex = m_Looping ? 0 : m_NumberOfPoints - 1;
                    transform.LookAt(currentTarget);
                    return;
                }
                currentTarget = _wayPoints[++_currentIndex];
                transform.LookAt(currentTarget);
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
        }

        public void DestroySelf()
        {
            gameObject.SetActive(false);
        }
    }
}