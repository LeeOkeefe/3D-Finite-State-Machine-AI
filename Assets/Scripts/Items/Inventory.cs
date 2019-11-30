using System;
using System.Collections.Generic;
using System.Linq;
using Items.Definitions;
using UnityEngine;

namespace Items
{
    internal sealed class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            if (items.Contains(item))
                return;

            if (items.Any(i => i.ID == item.ID))
                throw new ArgumentException("Item ID already exists.");

            items.Add(item);
            Debug.Log("ITEM ADDED: " + item.Name);
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
    }
}
