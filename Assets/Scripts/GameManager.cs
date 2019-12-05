using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items.Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject m_NotificationMessage;
        [SerializeField] private GameObject m_ErrorMessage;

        public List<Item> ExistingItems = new List<Item>();

        public void Awake()
        {
            if (Instance != null)
                Destroy(this);

            if (Instance == null)
                Instance = this;
        }

        public Item FindExistingItemById(int id)
        {
            return Instance.ExistingItems.FirstOrDefault(item => item.ID == id);
        }

        public IEnumerator ShowMessage(string message)
        {
            UserInterfaceUtils.ToggleCanvasGroup(m_NotificationMessage.GetComponent<CanvasGroup>(), true);
            m_NotificationMessage.GetComponentInChildren<Text>().text = message;
            yield return new WaitForSeconds(1);
            UserInterfaceUtils.ToggleCanvasGroup(m_NotificationMessage.GetComponent<CanvasGroup>(), false);
        }
    }
}
