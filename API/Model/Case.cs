using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("TB_M_Cases")]
    public class Case
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? Review { get; set; }
        [Required]
        public int Level { get; set; }
        public int? StaffId { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<History> History {get; set;}
        [JsonIgnore]
        public virtual ICollection<Convertation> Convertation { get; set; }

    }
}
