using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quest = new List<Quest>();

        static QuestFactory()
        {
            //Declare the items needed to complete the quest and the rewards
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(5001, 5));
            rewardItems.Add(new ItemQuantity(1002, 1));

            // Create the QUEST!
            _quest.Add(new Quest(1,
                "Bring me 5 pieces of bread",
                "They are scatterd around the prison",
                itemsToComplete,
                25, 10, rewardItems));
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quest.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
