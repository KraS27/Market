using Market.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Name")]
        [MaxLength(50, ErrorMessage ="Max Length 50 symbols")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [Range(1, 5000, ErrorMessage ="Incorect Value")]
        public int Price { get; set; }

        public string Description { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
