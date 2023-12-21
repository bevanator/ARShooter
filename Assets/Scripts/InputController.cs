using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ARShooter
{
    public class InputController : MonoBehaviour
    {
        public static event Action<bool> TouchStatusChanged;
        private void FixedUpdate()
        {
            ProcessTouch();
        }

        private void ProcessTouch()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    TouchStatusChanged?.Invoke(true);
                }


                if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    TouchStatusChanged?.Invoke(false);
                }
            }
        }
    }
}