using System;
using UnityEngine;

namespace Assets.Scripts.Items.Definitions
{
    [Serializable]
    public class Item
    {
        public string Name { get; }
        public int ID { get; }
        public Sprite Sprite { get; }

        public Item(string name, int id, Sprite sprite)
        {
            Name = name;
            ID = id;
            Sprite = sprite;
        }
    }
}
