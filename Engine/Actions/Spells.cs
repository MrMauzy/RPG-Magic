using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class Spells : BaseAction, IAction
    {
        private readonly int _damage;
        private readonly int _mana;

        public Spells(GameItem spellInUse, int magicDamage, int manaCost) :
            base(spellInUse)
        {
            _damage = magicDamage;
            _mana = manaCost;
        }

        public void Execute(LivingEntity actor, LivingEntity victim)
        {
            int damage = _damage;

            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string victimName = (victim is Player) ? "you" : $"the {victim.Name.ToLower()}";

            if(actor.CurrentMagicPoints < _mana)
            {
                ReportResults("You are out of mana. Use a potion or use your sword!");
                return;
            }

            if (damage > 0)
            {
                ReportResults($"{actorName} hit {victimName} for {damage} point{((damage) > 1 ? "s" : "")}.");
                victim.TakeDamage(damage);

                actor.SpendMana(_mana);
            }
            else
            {
                ReportResults("You missed");
            }
        }
    }
}
