using API.Context;
using API.Model;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class HistoryRepository : GeneralRepository<MyContext, History, int>
    {
        private readonly MyContext myContext;

        public HistoryRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<HistoryVM> GetHistory() {
            var all = (
                    from h in myContext.Histories
                    join u in myContext.Users on h.UserId equals u.Id
                    join c in myContext.Cases on h.CaseId equals c.Id
                    join sc in myContext.StatusCodes on h.StatusCodeId equals sc.Id
                    select new HistoryVM
                    {
                        Id = h.Id,
                        Description = h.Description,
                        DateTime = h.DateTime,
                        Level = h.Level,
                        UserId = u.Id,
                        UserName = u.Name,
                        CaseName = c.Description,
                        StatusCodeName = sc.Name
                    }
                ).ToList();
            return all.OrderByDescending(d => d.DateTime);
        }
    }
}
