using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    internal sealed class Menu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_MainMenu;
        [SerializeField] private CanvasGroup m_Options;

        public void StartGame()
        {
            //TODO: CHANGE NAME ("MAP" is currently main scene)
            SceneManager.LoadScene("Map");
        }

        public void OpenOptions()
        {
            UserInterfaceUtils.ToggleCanvasGroup(m_MainMenu, false);
            UserInterfaceUtils.ToggleCanvasGroup(m_Options, true);
        }

        public void CloseOptions()
        {
            UserInterfaceUtils.ToggleCanvasGroup(m_MainMenu, true);
            UserInterfaceUtils.ToggleCanvasGroup(m_Options, false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
