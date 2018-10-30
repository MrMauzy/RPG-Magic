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
                "/Engine;component/Images/Locations/pixelCell.png");

            newWorld.AddLocation(1, 0, "Prison Lobby",
                "This is where the guard hangout.",
                "/Engine;component/Images/Locations/Lobby.png");

            newWorld.AddLocation(1, -1, "Empty Cell",
                "This Cell Looks to be Abandoned.",
                "/Engine;component/Images/Locations/pixelCell.png");

            newWorld.AddLocation(0, 0, "Courtyard",
                "Outdoor Activities Yard.",
                "/Engine;component/Images/Locations/Courtyard.png");

            newWorld.LocationAt(0, 0).AddMonster(1, 100);

            newWorld.AddLocation(2, 0, "Exit",
                "Door is locked solid.",
                "/Engine;component/Images/Locations/Door.png");

            newWorld.AddLocation(-1, 0, "Cafeteria",
                "The food looks disgusting.",
                "/Engine;component/Images/Locations/Cafeteria.png");

            newWorld.LocationAt(-1, 0).AddMonster(2, 100);

            newWorld.AddLocation(-2, 0, "Empty Cell",
                "This cell looks recently abandoned.",
                "/Engine;component/Images/Locations/pixelCell.png");

            newWorld.LocationAt(0, 0).QuestsAvaliable.Add(QuestFactory.GetQuestByID(1));

            return newWorld;
        }
    }
}
