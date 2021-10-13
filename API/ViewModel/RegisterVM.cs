using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Not Valid")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        //[StringLength(150, ErrorMessage = "Minimal Password 6 and Maximal Password 150", MinimumLength = 6)]
        [RegularExpression("^(?=.[A-Za-z0-9])(?=.)[A-Za-z0-9]{6,150}$", ErrorMessage = "Password harus mengadung huruf besar,huruf kecil dan angka ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        [Required(ErrorMessage = "Gender is required")]
        [Range(0, 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Number Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Company is required")]
        public string Company { get; set; }
    }
}
