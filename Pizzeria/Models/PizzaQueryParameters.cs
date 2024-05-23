namespace Pizzeria.Models
{
    public class PizzaQueryParameters
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? IngredientName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
