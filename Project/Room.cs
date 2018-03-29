using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Item> Items { get; set; }

        public List<Enemy> Enemies { get; set; }

        public void UseItem(Item item)
        {
            string itemName = item.Name.ToLower();
            switch (itemName)
            {
                case "orb":
                    break;
                case "burned bone":
                    break;
                case "candlestick":
                    break;
                case "flat basketball":
                    break;
                case "vault key":
                    if (Name == "Vault")
                    {
                        Console.WriteLine("You open the vault and see a rosary in front of you");
                        Items.Add(new Item("Rosary", "A rosary that may have some sort of power"));
                    }
                    break;
                case "rosary":
                    break;
                case "beer":
                    break;
                case "pills":
                    break;
                case "pendant":
                    break;
            }
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public Room(GameSetup gs, int roomCount)
        {
            Name = gs.RoomNames[roomCount];
            Description = gs.RoomDescriptions[roomCount];
            Items = new List<Item>();
            Enemies = new List<Enemy>();
            if (gs.ItemNames[roomCount] != "")
            {
                Items.Add(new Item(gs.ItemNames[roomCount], gs.ItemDescriptions[roomCount]));
            }
            if (gs.EnemyNames[roomCount] != "")
            {
                Enemies.Add(new Enemy(gs.EnemyNames[roomCount], gs.EnemyHealth[roomCount], gs.EnemyDamage[roomCount]));
            }
        }
    }
}