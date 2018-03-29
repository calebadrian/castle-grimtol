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
                    if (Name == "Gardens")
                    {
                        if (Enemies.Count > 0)
                        {
                            Console.WriteLine("The orb magically defeats the reaper and he dissapears!");
                            Enemies.RemoveAt(0);
                        }
                        else
                        {
                            Console.WriteLine("Even though the reaper is dead this magically gives you a potion to bring you back to full life");
                            Items.Add(new Item("Max Potion", "This potion will bring you back to full health", 0));
                        }
                    }
                    else
                    {
                        Console.WriteLine("The item does nothing it doesn't seem like that was the right place to use this");
                    }
                    break;
                case "burned bone":
                    if (Name == "Dungeon")
                    {
                        Console.WriteLine("You lay the burned bone in front of the prisoner and he calms.  'Thank you for returning my missing bone to me here you might be able to use this.'");
                        Items.Add(new Item("Greatsword", "A sword of epic porportions that will do good damage", 20));
                    }
                    break;
                case "candlestick":
                    if (Name == "Abandoned Room")
                    {
                        Console.WriteLine("You light the candle and as the room begins to come clear you see a evil nun that begins to walk towards you");
                        Enemies.Add(new Enemy("Evil Nun", 40, 12));
                    }
                    break;
                case "flat basketball":
                    Console.WriteLine("Its a flat basketball what did you think was going to happen?");
                    break;
                case "vault key":
                    if (Name == "Vault")
                    {
                        Console.WriteLine("You open the vault and see a rosary in front of you");
                        Items.Add(new Item("Rosary", "A rosary that may have some sort of power", 0));
                    }
                    else
                    {
                        Console.WriteLine("Well you just wasted your chance at opening the vault");
                    }
                    break;
                case "rosary":
                    if (Name == "Arsenal")
                    {
                        if (Enemies.Count > 0)
                        {
                            Console.WriteLine("You shake the rosary at the demon which makes him cower in fear and run away");
                            Enemies.RemoveAt(0);

                        }
                        else
                        {
                            Console.WriteLine("Although the demon is dead the rosary grants you a potion to heal you");
                            Items.Add(new Item("Potion", "A potion to heal you", 0));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Good job wasting a useful item there champ");
                    }
                    break;
                case "beer":
                    Console.WriteLine("You drink the delicious beer must have been a porter or stout but all that happens is you feel a bit better");
                    break;
                case "pills":
                    break;
                case "pendant":
                    Console.WriteLine("You swing the pendant around wildly but nothing happens.  Seems that was useless");
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
                Items.Add(new Item(gs.ItemNames[roomCount], gs.ItemDescriptions[roomCount], gs.ItemDamages[roomCount]));
            }
            if (gs.EnemyNames[roomCount] != "")
            {
                Enemies.Add(new Enemy(gs.EnemyNames[roomCount], gs.EnemyHealth[roomCount], gs.EnemyDamage[roomCount]));
            }
        }
    }
}