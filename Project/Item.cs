using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Item : IItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Damage { get; set; }
        public Item(string name, string description, int damage)
        {
            Name = name;
            Description = description;
            Damage = damage;
        }
    }

}