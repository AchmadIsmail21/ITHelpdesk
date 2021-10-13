using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class TicketVM
    {
        [Required]
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
