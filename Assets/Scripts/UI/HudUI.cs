using System;
using UnityEngine;
using UnityEngine.UI;

namespace ARShooter.UI
{
    public class HudUI : MonoBehaviour
    {
        [SerializeField] private Image m_FillImage;

        private void OnEnable()
        {
            Player.OnHealthChanged += OnHealthChanged;
        }
        private void OnDisable()
        {
            Player.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float amount)
        {
            SetFillAmount(amount);
        }

        private void SetFillAmount(float amount)
        {
            m_FillImage.fillAmount = amount;
        }
    }
}