using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private static readonly List<Recipe> _recipes = new List<Recipe>();

        static RecipeFactory()
        {
            Recipe legendarySword = new Recipe(1, "Legendary Sword");
            legendarySword.AddIngredient(3000, 1);
            legendarySword.AddIngredient(3001, 1);
            legendarySword.AddIngredient(3002, 1);
            legendarySword.AddOutputItem(1005, 1);

            Recipe prisonKey = new Recipe(2, "Prison Key");
            prisonKey.AddIngredient(3003, 1);
            prisonKey.AddIngredient(3004, 1);
            prisonKey.AddIngredient(3005, 1);
            prisonKey.AddOutputItem(9001, 1);

            _recipes.Add(legendarySword);
            _recipes.Add(prisonKey);
        }

        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(x => x.ID == id);
        }
    }
}
