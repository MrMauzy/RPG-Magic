using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class Heal : IAction
    {
        private readonly GameItem _item;
        private readonly int _hitPointsHealed;

        public event EventHandler<string> OnActionPerformed;

        public Heal(GameItem item, int hitPointsHealed)
        {
            if(item.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{item.Name} is not consumable.");
            }

            _item = item;
            _hitPointsHealed = hitPointsHealed;
        }

        public void Execute(LivingEntity actor, LivingEntity victim)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string victimName = (victim is Player) ? "You" : $"The {victim.Name.ToLower()}";

            ReportResults($"{actorName} heal {victimName} for {_hitPointsHealed} point{(_hitPointsHealed > 1 ? "s" : "")}");
            victim.Heal(_hitPointsHealed);
        }

        private void ReportResults(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
