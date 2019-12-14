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
                StartCoroutine(GameManager.Instance.ShowMessage("Get the documents before leaving!"));
            }
            else
            {
                SceneManager.LoadScene("EndLevel");
            }
        }
    }
}
