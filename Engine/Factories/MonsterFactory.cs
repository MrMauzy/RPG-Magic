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
                    Monster GreenGoo =
                        new Monster("Green Goo", "GreenGoo.png", 5, 5, 5, 2);

                    GreenGoo.CurrentWeapon = ItemFactory.CreateGameItem(1012);

                    AddLootItem(GreenGoo, 8001, 50);
                    AddLootItem(GreenGoo, 5003, 25);
                    AddLootItem(GreenGoo, 5004, 25);

                    return GreenGoo;

                case 2:
                    Monster Outlaw =
                        new Monster("Outlaw Prisoner", "Outlaw.png", 20, 20, 10, 20);

                    Outlaw.CurrentWeapon = ItemFactory.CreateGameItem(1011);

                    AddLootItem(Outlaw, 8001, 50);
                    AddLootItem(Outlaw, 5005, 35);
                    AddLootItem(Outlaw, 5006, 15);

                    return Outlaw;

                case 3:
                    Monster RobotScout =
                        new Monster("Robot Scout", "ScoutMachine.png", 50, 50, 100, 200);

                    RobotScout.CurrentWeapon = ItemFactory.CreateGameItem(1010);

                    AddLootItem(RobotScout, 8001, 50);
                    AddLootItem(RobotScout, 5007, 30);
                    AddLootItem(RobotScout, 8002, 15);
                    AddLootItem(RobotScout, 9001, 5);

                    return RobotScout;

                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
            }
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }
    }
}
