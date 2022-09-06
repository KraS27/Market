using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter Name")]
        [MinLength(3, ErrorMessage = "MinLength 25 symbols")]
        [MaxLength(25, ErrorMessage = "MaxLength 25 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password must contain at least 4 symbols")]
        public string Password { get; set; }
    }
}
