using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
namespace ARShooter
{
    public class PortalPlacer : MonoBehaviour
    {
        private bool _isPlaced;
        [SerializeField] private GameObject m_PlacedPrefab;
        
        GameObject spawnedObject;

        private ARRaycastManager _arRaycastManager;
        private ARPlaneManager _arPlaneManager;
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

        private void Awake()
        {
            _arRaycastManager = GetComponent<ARRaycastManager>();
            _arPlaneManager = GetComponent<ARPlaneManager>();
        }

        private void Update()
        {
            if(_isPlaced) return;
            if (Input.touchCount == 0) return;

            if (_arRaycastManager.Raycast(Input.GetTouch(0).position, _hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = _hits[0].pose;
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                _arPlaneManager.enabled = false;
                _isPlaced = true;
            }
        }
    }
}