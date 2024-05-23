namespace Pizzeria.Models
{
    public class PizzaAPIPostRequest
    {
        public Pizza Pizza { get; set; }
        public List<string> SelectedIngredients { get; set; }
    }
}
