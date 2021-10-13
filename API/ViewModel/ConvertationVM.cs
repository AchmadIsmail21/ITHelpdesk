using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class ConvertationVM
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string CaseName { get; set; }
        public int CaseId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        
    }
}
