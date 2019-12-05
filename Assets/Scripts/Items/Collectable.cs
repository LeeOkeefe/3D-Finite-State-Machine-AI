using Items;
using Items.Definitions;
using UnityEngine;

namespace Assets.Scripts.Items
{
    internal sealed class Collectable : MonoBehaviour
    {
        [SerializeField] private string m_ItemName;
        [SerializeField] private int m_ItemId;
        [SerializeField] private Sprite m_ItemSprite;
        [SerializeField] private Inventory m_Inventory;

        public Item Item;

        private void Start()
        {
            Item = new Item(m_ItemName, m_ItemId, m_ItemSprite);
            GameManager.Instance.ExistingItems.Add(Item);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) 
                return;

            m_Inventory.AddItem(Item);
            Destroy(gameObject);
        }
    }
}
