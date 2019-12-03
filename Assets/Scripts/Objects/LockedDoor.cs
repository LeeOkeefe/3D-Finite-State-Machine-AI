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
                Debug.Log("You require a Mortuary Key to unlock the door.");
            }
            else
            {
                StartCoroutine(OpenDoor());
            }
        }
    }
}
