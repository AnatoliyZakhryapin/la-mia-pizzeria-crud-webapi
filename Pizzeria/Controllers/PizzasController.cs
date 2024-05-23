using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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

        [HttpGet("{id}")]
        public IActionResult Show(long id)
        {
            Pizza pizzaFinded = PizzaManager.GetPizzaById(id);

            if (pizzaFinded == null)
                return NotFound($"Pizza con ID {id} non essiste.");
            return Ok(pizzaFinded);
        }

        [HttpPost]
        public IActionResult Store([FromBody] PizzaAPIStoreRequest request ) 
        {
            PizzaManager.AddNewPizza(request.Pizza, request.SelectedIngredients);
            return Ok();
        }
    }
}
