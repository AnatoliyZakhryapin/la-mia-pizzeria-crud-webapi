using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;

namespace Pizzeria.Data
{
    public static class PizzaManager
    {
        public static void PizzaSeeder()
        {
            if (CountAllPizzas() == 0)
            {
                try
                {
                    PizzaManager.AddNewPizza(new Pizza("Margherita", "Pola di pomodoro, Fiordilatte, Origano, Basilico", "~/img/pizza_margherita.png", 7.99m));
                    PizzaManager.AddNewPizza(new Pizza("Prosciutto e mozzarella", "Pola di pomodoro, Fiordilatte, Prosciutto", "~/img/pizza_prosciutto.png", 9.99m));
                    PizzaManager.AddNewPizza(new Pizza("Divola", "Pola di pomodoro, Fiordilatte, Salame", "~/img/pizza_diavola.png", 10.99m));
                    PizzaManager.AddNewPizza(new Pizza("Quattro formaggi", "Mozzarella, Gorgonzola, Fontina, Parmigiano, Polpa di pomodoro", "~/img/pizza_quattro_formaggi.png", 11.99m));
                    PizzaManager.AddNewPizza(new Pizza("Napoletana", "Mozzarella, pomodoro, acciughe, origano, olio d’oliva Polpa di pomodoro", "~/img/pizza_napoletana.png", 10.99m));
                }
                catch (Exception) { }
            }
            if (CountAllIngredients() == 0)
            {
                try
                {
                    PizzaManager.AddNewIngredient(new Ingredient("Polpa di pomodoro", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Acciughe", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Basilico", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Fiordilatte", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Fontina", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Gorgonzola", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Mozzarella", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Mozzarella di Bufala", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Olio d'oliva", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Origano", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Parmigiano", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Polpa di pomodoro", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Pomodoro", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Prosciuto", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Salame", new List<Pizza>()));
                }
                catch (Exception) { }
            }
        }
        public static int CountAllIngredients()
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Ingredients.Count();
        }
        public static int CountAllPizzas()
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.Count();
        }
        public static void AddNewIngredient(Ingredient ingredient)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            db.Add(ingredient);
            db.SaveChanges();
        }

        public static void AddNewPizza(Pizza pizza, List<string> SelectedIngredients = null)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            if(SelectedIngredients != null )
            {
                pizza.Ingredients = new List<Ingredient>();
                foreach(string i in SelectedIngredients)
                {
                    int id = int.Parse(i);
                    Ingredient ingredient = db.Ingredients.FirstOrDefault(i => i.IngredientId == id);
                    pizza.Ingredients.Add(ingredient);
                }
            }
            db.Add(pizza);
            db.SaveChanges();
        }

        public static List<Pizza> GetAllPizzas(bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            
            if (includeReferences)
                return db.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).ToList();
            return db.Pizzas.ToList();
        }

        public static Pizza GetPizzaByName(string name)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.FirstOrDefault(p => p.Name == name);
        }

        public static Pizza GetPizzaById(int id, bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();

            if (includeReferences)
                return db.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault(p => p.PizzaId == id);
            return db.Pizzas.FirstOrDefault(p => p.PizzaId == id);
        }

        public static bool UpdatePizza(long id, Pizza pizzaUpdated, List<string> SelectedIngredients)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            var pizzaToUpdate = db.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();
    
            if (pizzaToUpdate != null)
            {
                pizzaToUpdate.Name = pizzaUpdated.Name;
                pizzaToUpdate.Description = pizzaUpdated.Description;
                pizzaToUpdate.Price = pizzaUpdated.Price;
                pizzaToUpdate.FotoUrl = pizzaToUpdate.FotoUrl;
                pizzaToUpdate.CategoryId = pizzaUpdated.CategoryId;

                pizzaToUpdate.Ingredients.Clear();
                if(SelectedIngredients != null)
                {
                    foreach (string i in SelectedIngredients)
                    {
                        int idIngredient = int.Parse(i);
                        Ingredient ingredient = db.Ingredients.FirstOrDefault(i => i.IngredientId == idIngredient);
                        pizzaToUpdate.Ingredients.Add(ingredient);
                    }
                }

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeletePizza(long id)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            var pizzaToDelete = db.Pizzas.Find(id);

            if(pizzaToDelete == null)
                return false;

            db.Pizzas.Remove(pizzaToDelete);
            db.SaveChanges();
            return true;
        }

        public static List<Category> GetAllCategories(bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
       
            if (includeReferences)
                return db.Categories.Include(p => p.Pizzas).ToList();

            return db.Categories.ToList(); ;
        }

        public static List<Ingredient> GetAllIngredients(bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            if(includeReferences)
                return db.Ingredients.Include(i => i.Pizzas).ToList();
            return db.Ingredients.ToList();
        }

        public static PizzeriaFormModel CreatePizzeriaFormModel(Pizza pizza = null)
        {
    
            PizzeriaFormModel model = new PizzeriaFormModel();
            if (pizza != null)
            {
                model.Pizza = pizza;
                model.Categories = GetAllCategories();
                model.CreateIngredients();

                return model;
            }

            model.Pizza = new Pizza();
            model.Categories = GetAllCategories();
            model.CreateIngredients();

            return model;
        }
    }
}
