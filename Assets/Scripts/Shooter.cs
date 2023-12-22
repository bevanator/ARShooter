using System;
using ARShooter.Interface;
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

        [SerializeField] private LayerMask _shootableLayer;
        private Transform _camTransform;
        private void Awake()
        {
            _camTransform = GetComponentInParent<Camera>().transform;
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
                ShootUsingRays();
                _time = m_ShootingDelay;
            }
            
        }

        private void ShootUsingRays()
        {
            Ray ray = new Ray(_camTransform.position, _camTransform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, _shootableLayer))
            {
                IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
                damageable.OnDamage();
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