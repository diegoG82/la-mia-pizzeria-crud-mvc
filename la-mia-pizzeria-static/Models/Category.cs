using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name required")]
        [StringLength(50, ErrorMessage = "Category Name can't be up to 50 characters")]
        public string Name { get; set; }

        //Relazione 1 ad n con Pizza
        public List<Pizza>? Pizzas { get; set; }

        //costruttore vuoto
        public Category()
        {

        }
    }
}