namespace Items.Definitions
{
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
