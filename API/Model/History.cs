using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("TB_TR_Histories")]
    public class History
    {
        [Key]
        
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        [Range(1,2)]
        public int Level { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }
        
        public int? CaseId { get; set; }
        public virtual Case Case { get; set; }
        
        public int? StatusCodeId { get; set; }
        public virtual StatusCode StatusCode { get; set; }
    }
}
