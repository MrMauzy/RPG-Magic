using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Actions;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();

        static ItemFactory()
        {
            // Weapons
            BuildWeapon(1001, "Sharp Stick", 1, 1, 2);
            BuildWeapon(1002, "Police Baton", 1, 1, 2);
            BuildWeapon(1005, "Legendary Sword", 100, 150, 200);

            BuildWeapon(1010, "Laser", 0, 0, 10);
            BuildWeapon(1011, "Club", 0, 0, 5);
            BuildWeapon(1012, "Goo", 0, 0, 3);

            // Health 
            BuildHealingItem(8001, "Bread", 1, 5);
            BuildMiscellaneousItem(8002, "Health Potion", 25);

            // Recipe Items
            BuildMiscellaneousItem(3000, "Sword Hilt", 2);
            BuildMiscellaneousItem(3001, "Iron Ingot", 2);
            BuildMiscellaneousItem(3002, "Steel Ingot", 3);

            //Random Junk
            BuildMiscellaneousItem(5003, "Slime", 1);
            BuildMiscellaneousItem(5004, "Jar of Slobber", 2);
            BuildMiscellaneousItem(5005, "Shaving Cream", 1);
            BuildMiscellaneousItem(5006, "Old Pizza", 4);
            BuildMiscellaneousItem(5007, "Robot Parts", 40);

            //Game Items
            BuildMiscellaneousItem(9001, "Prison Key", 3);
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }

        private static void BuildMiscellaneousItem(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price,
            int minDamage, int maxDamage)
        {
            GameItem weapon = new GameItem(GameItem.ItemCategory.Weapon, id, name, price, true);

            weapon.Action = new AttackWithWeapon(weapon, minDamage, maxDamage);

            _standardGameItems.Add(weapon);
        }

        private static void BuildHealingItem(int id, string name, int price, int hitPointsHealed)
        {
            GameItem item = new GameItem(GameItem.ItemCategory.Consumable, id, name, price);
            item.Action = new Heal(item, hitPointsHealed);
            _standardGameItems.Add(item);
        }

        public static string ItemName(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(i => i.ItemTypeID == itemTypeID)?.Name ?? "";
        }
    }
}
