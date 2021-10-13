using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class ProfileVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        
        [Range(0, 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }
        public string RoleName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
    }
}
