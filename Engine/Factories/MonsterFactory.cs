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
                        new Monster("Green Goo", "GreenGoo.png", 5, 5, 1, 3, 5, 2);

                    AddLootItem(GreenGoo, 8001, 50);
                    AddLootItem(GreenGoo, 5003, 25);
                    AddLootItem(GreenGoo, 5004, 25);

                    return GreenGoo;

                case 2:
                    Monster Outlaw =
                        new Monster("Outlaw Prisoner", "Outlaw.png", 20, 20, 5, 15, 10, 20);

                    AddLootItem(Outlaw, 8001, 50);
                    AddLootItem(Outlaw, 5005, 35);
                    AddLootItem(Outlaw, 5006, 15);

                    return Outlaw;

                case 3:
                    Monster RobotScout =
                        new Monster("Robot Scout", "ScoutMachine.png", 50, 50, 25, 45, 100, 200);

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
                monster.Inventory.Add(new ItemQuantity(itemID, 1));
            }
        }
    }
}
