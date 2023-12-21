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
        private bool _isShooting;
        private static readonly int IsShooting = Animator.StringToHash("IsShooting");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _time = m_ShootingDelay;
        }

        private void FixedUpdate()
        {
            if(!_isShooting) return;
            _time -= Time.deltaTime;
            if (_time <= 0)
            { 
                ShootFromBarrel(m_ShootingPoint1);
                ShootFromBarrel(m_ShootingPoint2);
                _time = m_ShootingDelay;
            }
        }

        private void ShootFromBarrel(Transform source)
        {
            GameObject bullet = Instantiate(m_Bullet, null);
            bullet.transform.position = source.position;
            bullet.transform.rotation = source.rotation;
            bullet.transform.Rotate(Vector3.right, 90f);

        }

        public void SetShootingStatus(bool state)
        {
            _isShooting = state;
            _animator.SetBool(IsShooting, state);
        }
    }
}