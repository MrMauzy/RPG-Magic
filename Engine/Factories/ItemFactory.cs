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
        private static List<GameItem> _standardGameItems;

        static ItemFactory()
        {
            _standardGameItems = new List<GameItem>();

            _standardGameItems.Add(new Weapon(1001, "Sharp Stick", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Police Baton", 1, 1, 2));
            _standardGameItems.Add(new GameItem(5001, "Bread", 1));
            _standardGameItems.Add(new GameItem(5002, "Prison Key", 3));
            _standardGameItems.Add(new GameItem(5003, "Rat Hair", 1));
            _standardGameItems.Add(new GameItem(5004, "Rat Teeth", 2));
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
