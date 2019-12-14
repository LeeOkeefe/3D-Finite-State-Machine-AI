using Items;
using UnityEngine;

namespace Objects
{
    internal sealed class LockedDoor : Door
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private int itemIdRequired;

        public override void Interact()
        {
            if (!inventory.HasItem(itemIdRequired))
            {
                var item = GameManager.Instance.FindExistingItemById(itemIdRequired);
                StartCoroutine(GameManager.Instance.ShowMessage(item.Name + " is required to unlock this door."));
            }
            else
            {
                StartCoroutine(OpenDoor());
            }
        }
    }
}
