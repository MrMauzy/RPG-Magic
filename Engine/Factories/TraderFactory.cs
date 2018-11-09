using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();

        static TraderFactory()
        {
            Trader OldGaurd = new Trader("Old Gaurd");
            OldGaurd.AddItemToInventory(ItemFactory.CreateGameItem(5004));

            Trader Mouse = new Trader("Shady Mouse");
            Mouse.AddItemToInventory(ItemFactory.CreateGameItem(5006));

            Trader MagicalBox = new Trader("Magical Trading Box");
            MagicalBox.AddItemToInventory(ItemFactory.CreateGameItem(1002));

            AddTraderToList(OldGaurd);
            AddTraderToList(Mouse);
            AddTraderToList(MagicalBox);
        }

        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(t => t.Name == name);
        }

        private static void AddTraderToList(Trader trader)
        {
            if(_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader named '{trader.Name}'");
            }

            _traders.Add(trader);
        }

    }
}
