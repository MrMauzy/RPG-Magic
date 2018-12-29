using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class Heal : BaseAction, IAction
    {
        private readonly int _hitPointsHealed;

        public Heal(GameItem itemInUse, int hitPointsHealed)
            : base(itemInUse)
        {
            if(itemInUse.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not consumable.");
            }

            _hitPointsHealed = hitPointsHealed;
        }

        public void Execute(LivingEntity actor, LivingEntity victim)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string victimName = (victim is Player) ? "You" : $"The {victim.Name.ToLower()}";

            ReportResults($"{actorName} heal {victimName} for {_hitPointsHealed} point{(_hitPointsHealed > 1 ? "s" : "")}");
            victim.Heal(_hitPointsHealed);
        }
    }
}
