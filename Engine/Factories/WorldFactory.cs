using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();

            newWorld.AddLocation(1, 1, "Your Cell",
                "This is your new home.",
                "Cell.jpg");

            newWorld.AddLocation(1, 0, "Prison Lobby",
                "This is where the guard hangout.",
                "Lobby.jpg");

            newWorld.AddLocation(1, -1, "Empty Cell",
                "This Cell Looks to be Abandoned.",
                "Cell.jpg");

            newWorld.AddLocation(0, 0, "Courtyard",
                "Outdoor Activities Yard.",
                "Courtyard.jpg");

            newWorld.LocationAt(0, 0).AddMonster(1, 100);

            newWorld.AddLocation(2, 0, "Exit",
                "The Exit!",
                "Entry.jpg");

            newWorld.AddLocation(-1, 0, "Cafeteria",
                "The food looks disgusting.",
                "Cafeteria.jpg");

            newWorld.LocationAt(-1, 0).AddMonster(2, 100);

            newWorld.AddLocation(-2, 0, "Empty Cell",
                "This cell looks recently abandoned.",
                "Cell.jpg");

            newWorld.LocationAt(0, 0).QuestsAvaliable.Add(QuestFactory.GetQuestByID(1));

            newWorld.AddLocation(-1, 1, "Office",
                "Someone's office.",
                "Office.jpg");

            newWorld.AddLocation(-1, -1, "Secret Hallway",
                "As Landon would say, I see nothing!",
                "Hallway.jpg");

            return newWorld;
        }
    }
}
