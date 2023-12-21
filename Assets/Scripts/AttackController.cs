using System;
using UnityEngine;

namespace ARShooter
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private Shooter m_Shooter;
        private bool _canAttack;

        private void Awake()
        {
            m_Shooter.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            PortalPlacer.OnPortalPlaced += OnPortalPlaced;
            InputController.TouchStatusChanged += OnTouchStatusChanged;
        }

        private void OnDisable()
        {
            PortalPlacer.OnPortalPlaced -= OnPortalPlaced;
            InputController.TouchStatusChanged -= OnTouchStatusChanged;
        }

        private void Start()
        {
            // m_Shooter.SetShootingStatus(true);
        }

        private void OnTouchStatusChanged(bool state)
        {
            // if(!_canAttack) return;
            m_Shooter.gameObject.SetActive(true);
            m_Shooter.SetShootingStatus(state);
        }

        private void OnPortalPlaced()
        {
            _canAttack = true;
        }
        
    }
}