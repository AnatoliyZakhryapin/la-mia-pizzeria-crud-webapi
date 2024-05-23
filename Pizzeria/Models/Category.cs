using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Pizzeria.Data.CustomValidationeRules;

namespace Pizzeria.Models
{
    [Table("Categories")]
    [Index(nameof(Title), IsUnique = true)]
    public class Category
    {
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Il titole della categoria è obbligatoria.")]
        [StringLength(50, ErrorMessage = "Il titolo non deve avere al più 50 caratteri")]
        public string Title { get; set; }

        public List<Pizza>? Pizzas { get; set; }

        public Category() { }
    }
}
