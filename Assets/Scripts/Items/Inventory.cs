using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Items;
using Items.Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    internal sealed class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> items = new List<Item>();
        [SerializeField] private List<Image> inventorySlots = new List<Image>();

        public void AddItem(Item item)
        {
            if (items.Contains(item))
                return;

            if (items.Any(i => i.ID == item.ID))
                throw new ArgumentException("Item ID already exists.");

            items.Add(item);
            UpdateInventoryUI(item);
        }

        public void RemoveItem(Item item)
        {
            if (!items.Contains(item))
                return;

            items.Remove(item);
        }

        public bool HasItem(Item item)
        {
            return items.Contains(item);
        }

        public bool HasItem(int id)
        {
            return items.Any(i => i.ID == id);
        }

        public Item FindItemById(int id)
        {
            return items.FirstOrDefault(item => item.ID == id);
        }

        private Image FindEmptySlot()
        {
            return inventorySlots.First(i => i.color.a == 0);
        }

        private void ModifyImageOpacity(Image slot)
        {
            var temp = slot.color;
            temp.a = 255;
            slot.color = temp;
        }

        private void UpdateInventoryUI(Item item)
        {
            var slot = FindEmptySlot();
            slot.sprite = item.Sprite;
            ModifyImageOpacity(slot);
        }
    }
}
