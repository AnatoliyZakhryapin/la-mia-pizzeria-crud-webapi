using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    [Table("ingredients")]
    [Index(nameof(Name), IsUnique = true)]
    public class Ingredient
    {
        public long IngredientId { get; set; }

        [Required(ErrorMessage = "Il nome del'ingrediente è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non deve avere al più 50 caratteri")]
        public string Name { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public Ingredient() { } 

        public Ingredient(string name, List<Pizza> pizzas)
        {
            Name = name;
            Pizzas = pizzas;
        }
    }
}
