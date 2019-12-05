using Assets.Scripts.Items.Definitions;
using UnityEngine;

namespace Assets.Scripts.Items
{
    internal sealed class Collectable : MonoBehaviour
    {
        [SerializeField] private string m_ItemName;
        [SerializeField] private int m_ItemId;
        [SerializeField] private Sprite m_ItemSprite;
        [SerializeField] private Inventory m_Inventory;

        private Item m_Item;

        private void Start()
        {
            m_Item = new Item(m_ItemName, m_ItemId, m_ItemSprite);
            GameManager.Instance.ExistingItems.Add(m_Item);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) 
                return;

            m_Inventory.AddItem(m_Item);
            Destroy(gameObject);
        }
    }
}
