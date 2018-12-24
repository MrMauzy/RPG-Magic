using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class AttackWithWeapon : IAction
    {
        private readonly GameItem _weapon;
        private readonly int _maxDamage;
        private readonly int _minDamage;

        public event EventHandler<string> OnActionPerformed;

        public AttackWithWeapon(GameItem weapon, int minDamage, int maxDamage)
        {
            if(weapon.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a fighting utensil.");
            }

            if(_maxDamage < 0)
            {
                throw new ArgumentException("Minimum Damage must be greater than 0.");
            }

            if(_maxDamage < _minDamage)
            {
                throw new ArgumentException("Max Damage must be greater than the Min Damage!");
            }

            _weapon = weapon;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity victim)
        {
            int damage = RandomNumberGenerator.NumberBetween(_minDamage, _maxDamage);

            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string victimName = (victim is Player) ? "You" : $"The {victim.Name.ToLower()}";

            if(damage == 0)
            {
                ReportResults($"You attacked, yet you missed {victim.Name.ToLower()}.");
            }
            else
            {
                ReportResults($"{damage} points of damage dealt to {victim.Name.ToLower()}.");
                victim.TakeDamage(damage);
            }
        }

        private void ReportResults(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
