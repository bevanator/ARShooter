using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
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
        private EnemyState _currentState;
        [SerializeField] private GameObject m_BulletPrefab;
        private float _time;
        [SerializeField] private float m_ShootingDelay = 3f;
        [SerializeField] private Transform m_ShootingPoint;

        private void Awake()
        {
            _currentState = EnemyState.Roaming;
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
            if((float)_health.CurrentHealth/_health.MaxHealth > 0.7f) return;
            if(_currentState is EnemyState.Roaming) _currentState = EnemyState.Approaching;
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
            switch (_currentState)
            {
                case EnemyState.Roaming:
                    MoveBetweenTargets();
                    break;
                case EnemyState.Approaching:
                    MoveToTarget(Player.Instance.transform.position);
                    break;
                case EnemyState.Attacking:
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            { 
                ShootFromPoint(m_ShootingPoint);
                _time = m_ShootingDelay;
            }
        }
        
        private void ShootFromPoint(Transform source)
        {
            GameObject bullet = Instantiate(m_BulletPrefab, null);
            bullet.transform.position = source.position;
            bullet.transform.LookAt(Player.Instance.transform);
        }

        private void MoveToTarget(Vector3 target)
        {
            m_MoveSpeed = 2f;
            transform.LookAt(target);
            if (Vector3.Distance(transform.position, target) <= 2f)
            {
                _currentState = EnemyState.Attacking;
                return;
            }
            float step = m_MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            
        }


        private void MoveBetweenTargets()
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

    public enum EnemyState
    {
        Roaming,
        Approaching,
        Attacking,
        Dead
    }
}