﻿using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static
{
    public class PizzaFormModel
    {
        public Pizza? Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }
        public List<string> SelectIngredientsId { get; set; }
    }
}
