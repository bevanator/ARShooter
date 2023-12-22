using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ARShooter.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Canvas m_Canvas;
        [SerializeField] private Button m_RestartButton;
        
        private void OnEnable()
        {
            Player.OnPlayerDeath += OnPlayerDeathEvent;
        }
        private void OnDisable()
        {
            Player.OnPlayerDeath -= OnPlayerDeathEvent;
        }

        private void Start()
        {
            m_RestartButton.onClick.AddListener(() =>
            {
                //todo: fix logic later
                SceneManager.LoadScene(0);
            });
        }

        private void OnPlayerDeathEvent()
        {
            Show();
        }

        private void Show()
        {
            m_Canvas.gameObject.SetActive(true);
        }
    }
}