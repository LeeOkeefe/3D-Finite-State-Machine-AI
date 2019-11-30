using Items.Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    internal sealed class WardenKey : MonoBehaviour
    {
        [SerializeField] private Image image;
        private static Inventory Inventory => FindObjectOfType<Inventory>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Inventory.AddItem(new Item("Warden's Key", 1));
                ModifyImageOpacity();
                Destroy(gameObject);
            }
        }

        private void ModifyImageOpacity()
        {
            var temp = image.color;
            temp.a = 255;
            image.color = temp;
        }
    }
}
