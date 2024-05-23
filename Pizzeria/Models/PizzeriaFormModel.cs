using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pizzeria.Data;

namespace Pizzeria.Models

{
    public class PizzeriaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        public List<SelectListItem>? Ingredients { get; set; }
        public List<string>? SelectedIngredients { get; set; }

        public PizzeriaFormModel() { }

        public PizzeriaFormModel (Pizza pizza, List<Category> categories)
        {
            this.Pizza = pizza;
            this.Categories = categories;
        }

        public void CreateIngredients()
        {
            this.Ingredients = new List<SelectListItem>();
            this.SelectedIngredients = new List<string>();

            List<Ingredient> ingredientsFormDb = PizzaManager.GetAllIngredients();

            foreach (Ingredient ingredient in ingredientsFormDb)
            {
                bool idSelected = this.Pizza.Ingredients?.Any(i => i.IngredientId == ingredient.IngredientId) == true;
                this.Ingredients.Add(new SelectListItem()
                {
                    Text = ingredient.Name,
                    Value = ingredient.IngredientId.ToString(),
                    Selected = idSelected
                });
                if (idSelected)
                    this.SelectedIngredients.Add(ingredient.IngredientId.ToString());
            }
        }
    }
}
