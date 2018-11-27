using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;

namespace Engine.Models
{
    public class Location
    {
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImageName { get; }
        public string ButtonImageName { get; }
        public List<Quest> QuestsAvaliable { get; } = new List<Quest>();

        public List<MonsterEncounter> MonsterHere { get; } =
            new List<MonsterEncounter>();

        public Trader TraderHere { get; set; }

        public Location(int xCoordinate, int yCoordinate, string name, string description,
            string imageName)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;
        }

        public void AddMonster(int monsterID, int chanceOfEncounter)
        {
            if(MonsterHere.Exists(m => m.MonsterID == monsterID))
            {
                // This monster has already been loaded in this location
                // so, overwrite the ChanceOfEncounter with the new number
                MonsterHere.First(m => m.MonsterID == monsterID)
                    .ChanceOfEncounter = chanceOfEncounter;
            }
            else
            {
                // This monster is not already at this location, so add it.
                MonsterHere.Add(new MonsterEncounter(monsterID, chanceOfEncounter));
            }
        }

        public bool MonsterOnLocation()
        {
            if(MonsterHere.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Monster GetMonster()
        {
            if(!MonsterHere.Any())
            {
                return null;
            }

            // Total the percentages of all monsters at this location
            int totalChances = MonsterHere.Sum(m => m.ChanceOfEncounter);

            // Select a random number between 1 and the total (in case the total 
            //chance is not 100
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            // Loop through the monster list
            // adding the monster's percentage chance of operating to the runningTotal variable
            // When the random number is lower than the runningTotal 
            // That is the monster to return
            int runningTotal = 0;

            foreach(MonsterEncounter monsterEncounter in MonsterHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncounter;

                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }

            // If there was a problem, return the last monster in the list.
            return MonsterFactory.GetMonster(MonsterHere.Last().MonsterID);
        }
    }
}
