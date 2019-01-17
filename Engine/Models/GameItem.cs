using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Actions;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon,
            MagicScrolls,
            Consumable
            
        }

        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }
        public IAction Action { get; set; }
        public int ManaCost { get; }

        public GameItem(ItemCategory category, int itemTypeID, string name, int price, 
            bool isUnique = false, IAction action = null, int manaCost = 0)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            Action = action;
            ManaCost = manaCost;
        }

        public void PerformAction(LivingEntity actor, LivingEntity victim)
        {
            Action?.Execute(actor, victim);
        }

        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeID, Name, Price, IsUnique,
                Action);
        }
    }
}
