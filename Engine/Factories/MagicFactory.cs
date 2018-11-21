using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class MagicFactory
    {
        private static readonly List<Magic> _standardMagicSpells = new List<Magic>();

        static MagicFactory()
        {
            _standardMagicSpells.Add(new Magic(8000, "Fireball", 5, 10));
            _standardMagicSpells.Add(new Magic(8001, "Lightning", 5, 15));
            _standardMagicSpells.Add(new Magic(8002, "Frost Bolt", 5, 20));
        }

        public static Magic CreateMagicSpell(int magicSpellID)
        {
            Magic standardSpell = _standardMagicSpells.FirstOrDefault(spell =>
            spell.MagicSpellID == magicSpellID);

            if (standardSpell != null)
            {
                return standardSpell.Clone();
            }
            return null;
        }
    }
}
