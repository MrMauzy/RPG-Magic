using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();

        static ItemFactory()
        {
            // Weapons
            _standardGameItems.Add(new Weapon(1001, "Sharp Stick", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Police Baton", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1005, "Legendary Sword", 100, 150, 200));

            // Health 
            _standardGameItems.Add(new GameItem(8001, "Bread", 1));
            _standardGameItems.Add(new GameItem(8002, "Health Potion", 25));


            //Random Junk
            _standardGameItems.Add(new GameItem(5003, "Slime", 1));
            _standardGameItems.Add(new GameItem(5004, "Jar of Slobber", 2));
            _standardGameItems.Add(new GameItem(5005, "Shaving Cream", 1));
            _standardGameItems.Add(new GameItem(5006, "Old Pizza", 4));
            _standardGameItems.Add(new GameItem(5007, "Robot Parts", 40));

            //Game Items
            _standardGameItems.Add(new GameItem(9001, "Prison Key", 3));
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item =>
            item.ItemTypeID == itemTypeID);

            if(standardItem != null)
            {
                if(standardItem is Weapon)
                {
                    return (standardItem as Weapon).Clone();
                }

                return standardItem.Clone();
            }
            return null;
        }
    }
}
