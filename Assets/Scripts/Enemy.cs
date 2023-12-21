using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ARShooter
{
    public class Enemy : MonoBehaviour
    {
        public float m_MoveSpeed = 5f;
        private List<Vector3> _wayPoints = new();
        private int _currentIndex;
        [SerializeField] private bool m_Looping = true;

        [SerializeField]
        private int m_NumberOfPoints = 5;
        

        private void Start()
        {
            for (int i = 0; i < m_NumberOfPoints; i++)
            {
                Vector2 point = Random.insideUnitCircle * 5;
                _wayPoints.Add(new Vector3(point.x, 3f, point.y));
            }
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
            if (Vector3.Distance(transform.position, currentTarget) <= 1f)
            {
                if (_currentIndex == m_NumberOfPoints - 1)
                {
                    _currentIndex = m_Looping ? 0 : m_NumberOfPoints - 1;
                    return;
                }
                _currentIndex++;
                currentTarget = _wayPoints[++_currentIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
        }
    }
}