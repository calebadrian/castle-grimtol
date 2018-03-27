using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public int Score { get; set; }

        public int Health { get; set; }

        public string Name { get; set; }

        public List<Item> Inventory { get; set; }

        public void TakeItem(Item item)
        {
            Inventory.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Inventory.Remove(item);
        }

        public Player(string name)
        {
            Score = 0;
            Health = 100;
            Name = name;
            Inventory = new List<Item>();
        }
    }
}