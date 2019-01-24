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
                    Monster WorriedAlligator =
                        new Monster("Worried Alligator", "BWorriedAlligator.png", 5, 5, 5, 2);

                    WorriedAlligator.CurrentWeapon = ItemFactory.CreateGameItem(1012);

                    AddLootItem(WorriedAlligator, 3005, 100);

                    return WorriedAlligator;

                case 2:
                    Monster Outlaw =
                        new Monster("Outlaw Prisoner", "Outlaw.png", 20, 20, 10, 20);

                    Outlaw.CurrentWeapon = ItemFactory.CreateGameItem(1011);

                    AddLootItem(Outlaw, 3003, 100);

                    return Outlaw;

                case 3:
                    Monster RobotScout =
                        new Monster("Robot Scout", "ScoutMachine.png", 50, 50, 100, 200);

                    RobotScout.CurrentWeapon = ItemFactory.CreateGameItem(1010);

                    AddLootItem(RobotScout, 3004, 100);

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
