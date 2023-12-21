using System;
using UnityEngine;

namespace ARShooter
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform m_ShootingPoint1;
        [SerializeField] private Transform m_ShootingPoint2;

        [SerializeField] private float m_ShootingDelay = 0.25f;
        private float _time;
        [SerializeField] private GameObject m_Bullet;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _time = m_ShootingDelay;
        }

        private void Update()
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            { 
                Shoot(m_ShootingPoint1);
                Shoot(m_ShootingPoint2);
                _time = m_ShootingDelay;
            }
        }

        private void Shoot(Transform source)
        {
            GameObject bullet = Instantiate(m_Bullet, null);
            bullet.transform.position = source.position;
            bullet.transform.rotation = source.rotation;
            bullet.transform.Rotate(Vector3.right, 90f);

        }
    }
}