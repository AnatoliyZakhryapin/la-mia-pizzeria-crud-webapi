using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index([FromQuery] PizzaQueryParameters queryParameters)
        {
            List<Pizza> listaPizzas = PizzaManager.GetAllPizzas();

            if (queryParameters.CategoryId.HasValue)
            {
                listaPizzas = listaPizzas.Where(p => p.CategoryId == queryParameters.CategoryId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                listaPizzas = listaPizzas.Where(p => p.Name.Contains(queryParameters.Name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(queryParameters.IngredientName))
            {
                listaPizzas = listaPizzas.Where(p => p.Ingredients.Any(i => i.Name.Contains(queryParameters.IngredientName, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            if (queryParameters.MinPrice.HasValue)
            {
                listaPizzas = listaPizzas.Where(p => p.Price >= queryParameters.MinPrice.Value).ToList();
            }

            if (queryParameters.MaxPrice.HasValue)
            {
                listaPizzas = listaPizzas.Where(p => p.Price <= queryParameters.MaxPrice.Value).ToList();
            }

            return Ok(listaPizzas);
        }
    }
}
