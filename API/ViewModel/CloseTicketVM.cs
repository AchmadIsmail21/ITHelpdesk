using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class CloseTicketVM
    {
        
        public int CaseId { get; set; }
        
        public int UserId { get; set; }
        public int? StaffId { get; set; }
        
        public string Email { get; set; }
    }
}
