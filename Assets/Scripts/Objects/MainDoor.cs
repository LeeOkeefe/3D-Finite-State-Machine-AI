using System.Linq;
using Items;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Objects
{
    internal class MainDoor : Door
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private int[] itemIdRequired;

        public override void Interact()
        {
            if (!itemIdRequired.All(i => inventory.HasItem(i)))
            {
                StartCoroutine(GameManager.Instance.ShowMessage("Find the documents before leaving!"));
            }
            else
            {
                PlayerPrefs.SetInt("LevelComplete", 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene("EndLevel");
            }
        }
    }
}
