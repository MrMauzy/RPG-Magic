using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class AttackWithWeapon : BaseAction, IAction
    {
        private readonly int _maxDamage;
        private readonly int _minDamage;

        public AttackWithWeapon(GameItem itemInUse, int minDamage, int maxDamage)
            : base(itemInUse)
        {
            if(itemInUse.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{itemInUse.Name} is not a fighting utensil.");
            }

            if(_maxDamage < 0)
            {
                throw new ArgumentException("Minimum Damage must be greater than 0.");
            }

            if(_maxDamage < _minDamage)
            {
                throw new ArgumentException("Max Damage must be greater than the Min Damage!");
            }

            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity victim)
        {
            int damage = RandomNumberGenerator.NumberBetween(_minDamage, _maxDamage);

            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string victimName = (victim is Player) ? "you" : $"the {victim.Name.ToLower()}";

            if(damage == 0)
            {
                ReportResults($"You attacked, yet you missed {victim.Name.ToLower()}.");
            }
            else
            {
                ReportResults($"{actorName} hit {victimName} for {damage} point{((damage) > 1 ? "s" : "")}.");
                victim.TakeDamage(damage);
            }
        }
    }
}
