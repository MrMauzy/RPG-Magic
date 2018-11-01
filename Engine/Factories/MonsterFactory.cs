using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch(monsterID)
            {
                case 1:
                    Monster AssassinRat =
                        new Monster("Assassin Rat", "AssassinRat.png", 5, 5, 1, 3, 5, 2);

                    AddLootItem(AssassinRat, 5001, 50);
                    AddLootItem(AssassinRat, 5003, 25);
                    AddLootItem(AssassinRat, 5004, 25);

                    return AssassinRat;

                case 2:
                    Monster RogueRat =
                        new Monster("Rogue Rat", "RogueRat.png", 5, 5, 1, 4, 5, 2);

                    AddLootItem(RogueRat, 5001, 50);
                    AddLootItem(RogueRat, 5003, 25);
                    AddLootItem(RogueRat, 5004, 20);
                    AddLootItem(RogueRat, 5002, 5);

                    return RogueRat;

                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
            }
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.Inventory.Add(new ItemQuantity(itemID, 1));
            }
        }
    }
}
