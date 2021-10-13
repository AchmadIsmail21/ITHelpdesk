using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("TB_M_Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nama tidak boleh kosong")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email tidak boleh kosong")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Not Valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password tidak Boleh kosong")]
        [StringLength(150, ErrorMessage = "Minimal Password 6 and Maximal Password 150", MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Tanggal lahir tidak boleh kosong")]
        public DateTime BirthDate { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        [Required(ErrorMessage = "Jenis Kelamin tidak boleh kosong")]
        public Gender gender { get; set; }
        [Required(ErrorMessage = "No handphone tidak boleh kosong")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Alamat tidak boleh kosong")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Departemen tidak boleh kosong")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Perusahaan tidak boleh kosong")]
        public string Company { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Convertation> Convertation { get; set; }
        [JsonIgnore]
        public virtual ICollection<History> History { get; set; }
        [JsonIgnore]
        public virtual ICollection<Case> Case { get; set; }
    }
}
