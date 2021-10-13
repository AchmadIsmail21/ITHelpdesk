using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class StatusCodeRepository : GeneralRepository<MyContext, StatusCode, int>
    {
        public StatusCodeRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
