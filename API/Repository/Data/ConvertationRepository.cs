using API.Context;
using API.Model;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ConvertationRepository : GeneralRepository<MyContext, Convertation, int>
    {
        private readonly MyContext myContext;
        public ConvertationRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int CreateConvertation(CreateConvertationVM createConvertationVM) {
            Convertation convertation = new Convertation() {
                DateTime = DateTime.Now,
                Message = createConvertationVM.Message,
                UserId = createConvertationVM.UserId,
                CaseId = createConvertationVM.CaseId
            };
            myContext.Convertations.Add(convertation);
            myContext.SaveChanges();
            return convertation.Id;
        }

        public IEnumerable<ConvertationVM> GetConvertations() {
            var all = (from cv in myContext.Convertations
                       join u in myContext.Users on cv.UserId equals u.Id
                       join c in myContext.Cases on cv.CaseId equals c.Id
                       select new ConvertationVM
                       {
                           Id = cv.Id,
                           DateTime = cv.DateTime,
                           Message = cv.Message,
                           UserId = u.Id,
                           UserName = u.Name,
                           CaseName = c.Description,
                           CaseId = c.Id
                       }
                       ).ToList();
            return all.OrderByDescending(d => d.DateTime);
        }

        public IEnumerable<Convertation> ViewConvertation() {
            var convertation = myContext.Convertations.ToList();
            return convertation;
        }

        public IEnumerable<ConvertationVM> ViewConvertationCaseId(int id) {
            var all = (
                    from cv in myContext.Convertations
                    join u in myContext.Users on cv.UserId equals u.Id
                    join c in myContext.Cases on cv.CaseId equals c.Id
                    select new ConvertationVM
                    {
                        Id = cv.Id,
                        DateTime = cv.DateTime,
                        Message = cv.Message,
                        UserId = u.Id,
                        UserName = u.Name,
                        CaseName = c.Description,
                        CaseId = c.Id
                    }).ToList();
            return all.Where(ci => ci.CaseId == id);
        }

        public IEnumerable<Convertation> ViewConvertationUserId(int id) {
            var user = myContext.Convertations.Where(u => u.UserId == id);
            return user;
        }
    }
}
