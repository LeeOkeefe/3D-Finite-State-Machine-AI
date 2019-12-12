using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    internal sealed class ScreenBrightness : MonoBehaviour
    {
        private Slider m_Slider;

        private void Start()
        {
            m_Slider = GetComponent<Slider>();
            m_Slider.value = PlayerPrefs.GetFloat("ScreenBrightness", 0.5F);
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Set our screen brightness to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            Screen.brightness = m_Slider.value;
            PlayerPrefs.SetFloat("ScreenBrightness", Screen.brightness);
            PlayerPrefs.Save();
        }
    }
}