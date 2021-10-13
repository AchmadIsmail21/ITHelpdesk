using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Not Valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(150, ErrorMessage = "Minimal Password 6 and Maximal Password 150", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
