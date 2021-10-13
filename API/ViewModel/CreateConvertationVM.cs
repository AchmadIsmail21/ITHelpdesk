using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class CreateConvertationVM
    {
        public int UserId { get; set; }
        public int CaseId { get; set; }
        public string Message { get; set; }
    }
}
