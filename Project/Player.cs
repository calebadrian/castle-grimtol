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
            Name = name;
            if (name == "TROGDOR")
            {
                Health = 10000000;
            }
            else
            {
                Health = 100;
            }
            Inventory = new List<Item>();
        }
    }
}