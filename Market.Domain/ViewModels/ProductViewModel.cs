using Market.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Введите имя")]
        [MaxLength(50, ErrorMessage ="Максимальной значение 50 символов")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Range(1, 5000, ErrorMessage ="Неверное значение")]
        public int Price { get; set; }

        public string Description { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public IEnumerable<ProductType> ProductTypes { get; set; }        
    }
}
