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

            _recipes.Add(legendarySword);
        }

        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(x => x.ID == id);
        }
    }
}
