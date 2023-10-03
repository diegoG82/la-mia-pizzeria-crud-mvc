using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;

        public PizzaController(PizzaContext db, ICustomLogger logger)
        {
            _myLogger = logger;
            _myDatabase = db;
        }


        public IActionResult Index()
        {
            _myLogger.WriteLog("You are in Pizza > Index");
            List<Pizza> pizzas = _myDatabase.Pizzas.ToList<Pizza>();
            return View("Index", pizzas);
        }

        public IActionResult UserIndex()
        {
            {
                _myLogger.WriteLog("You are in Pizza > UserIndex");
                List<Pizza> pizzas = _myDatabase.Pizzas.ToList<Pizza>();
                return View("UserIndex", pizzas);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (newPizza.Image == null)
            {
                newPizza.Image = "/img/default.jpg";
            }

            if (!ModelState.IsValid)
            {

                return View("Create", newPizza);
            }

            _myDatabase.Pizzas.Add(newPizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
           
                Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound("Pizza not founded");
                }
                else
                {
                    return View("Update", pizzaToEdit);
                }
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza modifiedPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", modifiedPizza);
            }

          

                Pizza? pizzaToUpdate = _myDatabase.Pizzas.Find(id);

                if (pizzaToUpdate != null)
                {
                    EntityEntry<Pizza> entryEntity = _myDatabase.Entry(pizzaToUpdate);
                    entryEntity.CurrentValues.SetValues(modifiedPizza);

                   _myDatabase.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("No pizza to modify");
                }

        }



        public IActionResult Details(int id)
        {
           
                Pizza? foundedPizza = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (foundedPizza == null)
                {
                    return NotFound($"L'articolo con {id} non è stato trovato!");
                }
                else
                {
                    return View("Details", foundedPizza);
                }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            
                Pizza? pizzaToDelete = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    _myDatabase.Pizzas.Remove(pizzaToDelete);
                    _myDatabase.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Pizza to delete not found!");
                }
           
        }



    }
}