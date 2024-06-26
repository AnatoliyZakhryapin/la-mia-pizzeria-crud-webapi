﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pizzeria.Data;
using Pizzeria.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index([FromQuery] PizzaQueryParameters queryParameters)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();

            var query = db.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).AsQueryable();

            if (queryParameters.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == queryParameters.CategoryId.Value);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.IngredientName))
            {
                query = query.Where(p => p.Ingredients.Any(i => i.Name.ToLower().Contains(queryParameters.IngredientName.ToLower())));
            }

            if (queryParameters.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= queryParameters.MinPrice.Value);
            }

            if (queryParameters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= queryParameters.MaxPrice.Value);
            }

            var listaPizzas = query.ToList();
            return Ok(listaPizzas);
            //List<Pizza> listaPizzas = PizzaManager.GetAllPizzas();

            //if (queryParameters.CategoryId.HasValue)
            //{
            //    listaPizzas = listaPizzas.Where(p => p.CategoryId == queryParameters.CategoryId.Value).ToList();
            //}

            //if (!string.IsNullOrEmpty(queryParameters.Name))
            //{
            //    listaPizzas = listaPizzas.Where(p => p.Name.Contains(queryParameters.Name, StringComparison.OrdinalIgnoreCase)).ToList();
            //}

            //if (!string.IsNullOrEmpty(queryParameters.IngredientName))
            //{
            //    listaPizzas = listaPizzas.Where(p => p.Ingredients.Any(i => i.Name.Contains(queryParameters.IngredientName, StringComparison.OrdinalIgnoreCase))).ToList();
            //}

            //if (queryParameters.MinPrice.HasValue)
            //{
            //    listaPizzas = listaPizzas.Where(p => p.Price >= queryParameters.MinPrice.Value).ToList();
            //}

            //if (queryParameters.MaxPrice.HasValue)
            //{
            //    listaPizzas = listaPizzas.Where(p => p.Price <= queryParameters.MaxPrice.Value).ToList();
            //}

            //return Ok(listaPizzas);
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
        public IActionResult Store([FromBody] PizzaAPIPostRequest request)
        {
            PizzaManager.AddNewPizza(request.Pizza, request.SelectedIngredients);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] PizzaAPIPostRequest request)
        {
            bool result = PizzaManager.UpdatePizza(id, request.Pizza, request.SelectedIngredients);
            if (result != true)
                return NotFound("Modifica non e andata a buon fine!");

            return Ok("Modificato con successo!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) 
        {
            bool result = PizzaManager.DeletePizza(id);

            if (result == true) return Ok($"Pizza con ID {id} è stata cancellata con successo");

            return NotFound();
        }
    }
}
