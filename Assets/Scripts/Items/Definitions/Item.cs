using System;

namespace Items.Definitions
{
    [Serializable]
    public class Item
    {
        public string Name { get; }
        public int ID { get; }

        public Item(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}
