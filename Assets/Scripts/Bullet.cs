using System.Threading.Tasks;
using UnityEngine;

namespace ARShooter
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float m_Speed = 5f;
        private Vector3 _spawnPos;
        private Rigidbody _rb;
        private float _startTime;
        private Vector3 _targetPos;
        private float _maxDistance = 500f;
        [SerializeField] private float m_Range = 5f;

        private void Awake()
        {
            _spawnPos = transform.position;
            Invoke("DestroySelf", 2f);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _startTime = Time.time;
            _targetPos = transform.position + _maxDistance * transform.forward;
            
        }
        
        private void FixedUpdate()
        {
            float distCovered = (Time.time - _startTime) * m_Speed;
            float distance = Vector3.Distance(_spawnPos, transform.position);
            float fractionOfJourney = distCovered / _maxDistance;
            _rb.MovePosition(Vector3.Lerp(transform.position, _targetPos, fractionOfJourney));
        }
    }
}