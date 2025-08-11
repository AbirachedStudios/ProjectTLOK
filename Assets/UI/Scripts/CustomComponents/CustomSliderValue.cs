using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scripts.CustomComponents
{
    public class CustomSliderValue : MonoBehaviour
    {
        [SerializeField] private Slider m_slider;
        [SerializeField] private TMP_Text m_percentageText;

        private void Start()
        {
            SliderPercentageToText();
        }

        public void SliderPercentageToText()
        {
            float percentage = (m_slider.value / m_slider.maxValue) * 100f;
            m_percentageText.text = $"{(int)percentage} %";
        }

        public void AdjustVolume(float percentage = 10)
        {
            m_slider.value += (percentage / 100f) * m_slider.maxValue;
        }
    }
}
