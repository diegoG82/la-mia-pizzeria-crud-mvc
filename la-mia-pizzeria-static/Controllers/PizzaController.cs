using Azure;
using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            List<Pizza> pizzas = _myDatabase.Pizzas.Include(Pizza => Pizza.Category).Include(Pizza => Pizza.Ingredients).ToList<Pizza>();
            return View("Index", pizzas);
        }

        public IActionResult UserIndex()
        {
            {
                _myLogger.WriteLog("You are in Pizza > UserIndex");
                List<Pizza> pizzas = _myDatabase.Pizzas.Include(Pizza => Pizza.Category).Include(Pizza => Pizza.Ingredients).ToList<Pizza>();
                return View("UserIndex", pizzas);
            }
        }

        public IActionResult Error()
        {

            return View("Error");
        }


        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDatabase.Categories.ToList();
            List<SelectListItem> allIngredientsSelectList = new List<SelectListItem>();
            List<Ingredient> ingredientdb = _myDatabase.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredientdb)
            {
                allIngredientsSelectList.Add(
                       new SelectListItem
                       {
                           Text = ingredient.Name,
                           Value = ingredient.Id.ToString()
                       });
            }


            PizzaFormModel model = new PizzaFormModel { Pizza = new Pizza(), Categories = categories, Ingredients= allIngredientsSelectList };

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {


            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;

                List<SelectListItem> allIngredientsSelectList = new List<SelectListItem>();
                List<Ingredient> ingredientdb = _myDatabase.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredientdb)
                {
                    allIngredientsSelectList.Add(
                           new SelectListItem
                           {
                               Text = ingredient.Name,
                               Value = ingredient.Id.ToString()
                           });
                }

                data.Ingredients = allIngredientsSelectList;

                return View("Create", data);
            }

            data.Pizza.Ingredients = new List<Ingredient>();

            if(data.SelectedIngredientsId != null)
            {
                foreach (string ingredientSelectedId in data.SelectedIngredientsId)
                {
                    int intIngredientSelectedId = int.Parse(ingredientSelectedId);

                    Ingredient? IngredientInDb = _myDatabase.Ingredients.Where(Ingredient => Ingredient.Id == intIngredientSelectedId).FirstOrDefault();

                    if (IngredientInDb != null)
                    {
                        data.Pizza.Ingredients.Add(IngredientInDb);
                    }
                }
            }


            _myDatabase.Pizzas.Add(data.Pizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToEdit == null)
            {
                return View("Error");
            }
            else
            {
                List<Category> categories = _myDatabase.Categories.ToList();

                PizzaFormModel model
                     = new PizzaFormModel { Pizza = pizzaToEdit, Categories = categories };

                return View("Update", model);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;
                return View("Update", data);
            }



            Pizza? pizzaToUpdate = _myDatabase.Pizzas.Find(id);

            if (pizzaToUpdate != null)
            {
                EntityEntry<Pizza> entryEntity = _myDatabase.Entry(pizzaToUpdate);
                entryEntity.CurrentValues.SetValues(data.Pizza);

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

            Pizza? foundedPizza = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).Include(Pizza => Pizza.Category).Include(Pizza => Pizza.Ingredients).FirstOrDefault();

            if (foundedPizza == null)
            {
                return NotFound($"Article with id: {id} not founded");
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
                return View("Error");
            }

        }



    }
}