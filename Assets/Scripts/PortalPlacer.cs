using System;
using System.Collections.Generic;
using ARShooter.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
namespace ARShooter
{
    public class PortalPlacer : MonoBehaviour
    {
        private bool _isPlaced;
        private bool _isPlaneDetected;
        private ARRaycastManager _arRaycastManager;
        private ARPlaneManager _arPlaneManager;
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
        [SerializeField] private GameObject m_PortalPrefab;
        public static event Action OnPlaneDetected;
        public static event Action OnPortalPlaced;


        private void Awake()
        {
            _arRaycastManager = GetComponent<ARRaycastManager>();
            _arPlaneManager = GetComponent<ARPlaneManager>();
        }
        

        private void FixedUpdate()
        {
            DetectPlane();
            ProcessTouch();
        }

        
        
        private void DetectPlane()
        {
            if (_isPlaneDetected) return;
            _isPlaneDetected = _arPlaneManager.trackables.count >= 1;
            if (!_isPlaneDetected) return;
            OnPlaneDetected?.Invoke();
        }

        private void ProcessTouch()
        {
            if (_arRaycastManager.Raycast(Input.GetTouch(0).position, _hits, TrackableType.PlaneWithinPolygon))
            {
                if(_isPlaced) return;
                if (Input.touchCount == 0) return;
                Pose hitPose = _hits[0].pose;
                Instantiate(m_PortalPrefab, hitPose.position, hitPose.rotation);
                _arPlaneManager.enabled = false;
                _isPlaced = true;
                OnPortalPlaced?.Invoke();
            }
        }
    }
}