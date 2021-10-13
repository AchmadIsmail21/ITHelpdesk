using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class CaseVM
    {
        public int Id { get; set; }
        
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? Review { get; set; }
        public int Level { get; set; }
        
        public int UserId { get; set; }
        public int? StaffId { get; set; }
        public string UserName { get; set; }
        public string PriorityName { get; set; }
        
        public string CategoryName { get; set; }

    }
}
