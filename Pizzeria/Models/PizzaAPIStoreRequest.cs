namespace Pizzeria.Models
{
    public class PizzaAPIStoreRequest
    {
        public Pizza Pizza { get; set; }
        public List<string> SelectedIngredients { get; set; }
    }
}
