using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Authorize]
    public class PizzeriaController : Controller
    {
        public IActionResult Index()
        {
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = ControllerContext.RouteData.Values["action"].ToString();
            string currentPage = $"{currentController}/{currentAction}";

            ViewData["CurrentPage"] = currentPage;

            List<Pizza> listaPizzas = PizzaManager.GetAllPizzas();

            return View(listaPizzas);
        }

        public IActionResult PreShow(int id, string pizzaName)
        {
            TempData["PizzaId"] = id;

            return RedirectToAction("Show", new { name = pizzaName });
        }

        [Route("/Pizzeria/DettaglioPizza/{name}")]
        public IActionResult Show(string name)
        {
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = ControllerContext.RouteData.Values["action"].ToString();
            string currentPage = $"{currentController}/{currentAction}";
            ViewData["CurrentPage"] = currentPage;

            if (TempData.ContainsKey("PizzaId"))
            {
                int id = (int)TempData["PizzaId"];

                Pizza pizzaFinded = PizzaManager.GetPizzaById(id);

                if (pizzaFinded != null)
                    return View(pizzaFinded);
                else
                    return View("errore");
            }
            else
            {
                return View("errore");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = ControllerContext.RouteData.Values["action"].ToString();
            string currentPage = $"{currentController}/{currentAction}";
            ViewData["CurrentPage"] = currentPage;

            PizzeriaFormModel model = PizzaManager.CreatePizzeriaFormModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(PizzeriaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategories(false);
                data.CreateIngredients();

                return View("Create", data);
            }

            PizzaManager.AddNewPizza(data.Pizza, data.SelectedIngredients);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult PreUpdate(int id, string pizzaName)
        {
            TempData["PizzaId"] = id;

            return RedirectToAction("Update", new { name = pizzaName });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(string name)
        {
            if (TempData.ContainsKey("PizzaId"))
            {
                int id = (int)TempData["PizzaId"];

                Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);

                if (pizzaToEdit != null)
                {
                    PizzeriaFormModel model = PizzaManager.CreatePizzeriaFormModel(pizzaToEdit);
                    return View(model);
                }
                else
                    return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(long id, PizzeriaFormModel data)
        {
            if(!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategories(false);
                data.CreateIngredients();
                data.Pizza.PizzaId = id;

                return View("Update", data);
            }

            bool result = PizzaManager.UpdatePizza(id, data.Pizza, data.SelectedIngredients);

            if (result == true)
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            bool result = PizzaManager.DeletePizza(id);

            if (result == true) return RedirectToAction("Index");

            return NotFound();
        }
    }
}
