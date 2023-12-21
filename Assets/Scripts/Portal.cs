using System;
using UnityEngine;

namespace ARShooter
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Enemy m_EnemyPrefab;

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Enemy enemy = Instantiate(m_EnemyPrefab);
                enemy.transform.position = transform.position;
            }
        }
    }
}