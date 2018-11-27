using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem
    {
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public Weapon(int itemTypeID, string name, int price, int minDamage, 
            int maxDamage) : base(itemTypeID, name, price, true)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public new Weapon Clone()
        {
            return new Weapon(ItemTypeID, Name, Price, MinDamage, MaxDamage);
        }
    }
}
