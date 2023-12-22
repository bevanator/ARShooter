using System;
using UnityEngine;
using UnityEngine.UI;

namespace ARShooter.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Canvas m_Canvas;
        [SerializeField] private Button m_startButton;
        public static event Action OnGameStartedEvent;
        private void Start()
        {
            m_startButton.onClick.AddListener(() =>
            {
                Hide();
                OnGameStartedEvent?.Invoke();
            });
        }

        public void Show()
        {
            m_Canvas.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            m_Canvas.gameObject.SetActive(false);
        }
    }
}