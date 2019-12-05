using UnityEngine;
using UnityEngine.UI;
using AudioListener = UnityEngine.AudioListener;

namespace Assets.Scripts.Menu
{
    internal sealed class SoundEffectsVolume : MonoBehaviour
    {
        private Slider m_Slider;

        private void Start()
        {
            m_Slider = GetComponent<Slider>();
            m_Slider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 0.5F);
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Set our audioSource volume to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            AudioListener.volume = m_Slider.value;
            PlayerPrefs.SetFloat("SoundEffectsVolume", AudioListener.volume);
            PlayerPrefs.Save();
        }
    }
}