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
        public string ImageName { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }

        public Monster(string name, string imageName,
            int maxHitPoints, int hitPoints,
            int minDamage, int maxDamage,
            int rewardExpPoints, int rewardGold)
        {
            Name = name;
            ImageName = string.Format($"/Engine;component/Images/Monsters/{imageName}");
            MaxHitPoints = maxHitPoints;
            CurrentHitPoints = hitPoints;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            RewardExperiencePoints = rewardExpPoints;
            Gold = rewardGold;
        }
    }
}
