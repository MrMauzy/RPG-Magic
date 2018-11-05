using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Magic
    {
        public int MagicSpellID { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }

        public Magic(int magicSpellID, string name, int manaCost)
        {
            MagicSpellID = magicSpellID;
            Name = name;
            ManaCost = manaCost;
        }

        public Magic Clone()
        {
            return new Magic(MagicSpellID, Name, ManaCost);
        }
    }
}
