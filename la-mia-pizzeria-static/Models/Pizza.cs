﻿using la_mia_pizzeria_static.ValidationAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {                               
        public int Id { get; set; }
        [Required(ErrorMessage ="Name required")]
        [MaxLength(100, ErrorMessage = "Max length of title is 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required")]
        [MoreThanFive]
        public string Description { get; set; }

 
        [DefaultValue("/img/default.jpg")]
        public string? Image {  get; set; }

        [Range(0, 100, ErrorMessage = "Invalid price")]
        public int Price { get; set; }

        public Pizza() { }
        public Pizza(string name, string description, string image, int price )
        {
            this.Name = name;   
            this.Description = description; 
            this.Image = image;
            this.Price = price;
        }

    }
}
