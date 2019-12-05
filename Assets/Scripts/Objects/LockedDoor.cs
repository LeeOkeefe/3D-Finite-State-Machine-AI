using Items;
using Objects;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    internal sealed class LockedDoor : Door, IInteractive
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
