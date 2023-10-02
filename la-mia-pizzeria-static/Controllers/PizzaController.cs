using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                {
                    List<Pizza> pizzas = db.Pizzas.ToList();
                    return View("Index", pizzas);
                }
            }


        }

        public IActionResult UserIndex()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList();
                return View("UserIndex", pizzas);
            }
        }



        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? foundedPizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (foundedPizza == null)
                {
                    return NotFound($"L'articolo con {id} non è stato trovato!");
                }
                else
                {
                    return View("Details", foundedPizza);
                }
            }
        }





    }
}