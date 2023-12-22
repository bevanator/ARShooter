using System;
using UnityEngine;

namespace ARShooter.UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_DevicePanel;
        [SerializeField] private CanvasGroup m_TapPanel;
        
        private void OnEnable()
        {
            PortalPlacer.OnPlaneDetected += OnPlaneDetected;
            PortalPlacer.OnPortalPlaced += OnPortalPlaced;
        }

        private void OnDisable()
        {
            PortalPlacer.OnPlaneDetected -= OnPlaneDetected;
            PortalPlacer.OnPortalPlaced -= OnPortalPlaced;
        }

        private void Start()
        {
            m_DevicePanel.gameObject.SetActive(true);
        }
        

        private void OnPortalPlaced()
        {
            m_TapPanel.gameObject.SetActive(false);
        }

        private void OnPlaneDetected()
        {
            m_DevicePanel.gameObject.SetActive(false);
            m_TapPanel.gameObject.SetActive(true);
        }
    }
}