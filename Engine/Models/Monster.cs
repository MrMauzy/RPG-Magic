using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public int RewardExperiencePoints { get; }

        public Monster(string name, string imageName,
            int maxHitPoints, int currentHitPoints,
            int minDamage, int maxDamage,
            int rewardExpPoints, int gold) :
            base (name, maxHitPoints, currentHitPoints, gold)
        {
            ImageName = string.Format($"/Engine;component/Images/Monsters/{imageName}");
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            RewardExperiencePoints = rewardExpPoints;
        }
    }
}
