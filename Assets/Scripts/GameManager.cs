using System;
using UnityEngine;

namespace ARShooter
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}