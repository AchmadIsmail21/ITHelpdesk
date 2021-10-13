using API.Context;
using API.Helper;
using API.Model;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class CaseRepository : GeneralRepository<MyContext, Case, int>
    {
        private readonly MyContext myContext;

        public CaseRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        MailHandler serviceEmail = new MailHandler();
        //create tiket --------------------------------------------------------------------------------------
        public int CreateTicket(TicketVM ticketVM) {
            var result = 0;
            var message = "You Succes Create Ticket";
            var user = myContext.Users.Find(ticketVM.UserId);
            if (user == null && user.Email == null) {
                return 400;
            }
            serviceEmail.SendEmail(message,user.Email);
            {
                Case cases = new Case()
                {
                    Description = ticketVM.Description,
                    StartDateTime = DateTime.Now,
                    Review = 0,
                    PriorityId = 1,
                    Level = 1,
                    UserId = ticketVM.UserId,
                    CategoryId = ticketVM.CategoryId
                };
                myContext.Add(cases);
                result = myContext.SaveChanges();

                //StaffCase staffCase = new StaffCase()
                /*{
                    CaseId = cases.Id,
                    StaffId = 1
                };
                myContext.Add(staffCase);*/
                result = myContext.SaveChanges();

                History history = new History()
                {
                    CaseId = cases.Id,
                    Description = "(System) Create Ticket and Ask Admin Support",
                    DateTime = DateTime.Now,
                    Level = 1,
                    UserId = ticketVM.UserId,
                    StatusCodeId = 1
                };
                myContext.Add(history);
                result = myContext.SaveChanges();
            }
            return result;
        }
        //Get kasus------------------------------------------------------------------------------------------------
        public IEnumerable<CaseVM> GetCases() {
            var all = (
                    from c in myContext.Cases
                    join u in myContext.Users on c.UserId equals u.Id
                    join p in myContext.Priorities on c.PriorityId equals p.Id
                    join ct in myContext.Categories on c.CategoryId equals ct.Id
                    select new CaseVM
                    {
                        Id = c.Id,
                        Description = c.Description,
                        StartDateTime = c.StartDateTime,
                        EndDateTime = c.EndDateTime,
                        Review = c.Review,
                        Level = c.Level,
                        UserId = u.Id,
                        UserName = u.Name,
                        PriorityName = p.Name,
                        CategoryName = ct.Name
                    }
                ).ToList();
            return all;
        }

        //by client------------------------------------------------------------------------------------------
        public IEnumerable<CaseVM> ViewTicketByUserId(int userId) {
            var all = (
                    from c in myContext.Cases
                    join u in myContext.Users on c.UserId equals u.Id
                    join p in myContext.Priorities on c.PriorityId equals p.Id
                    join ct in myContext.Categories on c.CategoryId equals ct.Id
                    
                    select new CaseVM
                    {
                        Id = c.Id,
                        Description = c.Description,
                        StartDateTime = c.StartDateTime,
                        EndDateTime = c.EndDateTime,
                        Review = c.Review,
                        Level = c.Level,
                        UserId = u.Id,
                        UserName = u.Name,
                        PriorityName = p.Name,
                        CategoryName = ct.Name
                        
                    }
                ).ToList();
            return all.Where(x => x.UserId == userId && (x.Review == null || x.Review <= 0));
        }

        //history from client----------------------------------------------------------------------------------------
        public IEnumerable<CaseVM> ViewHistoryTicketsByUserId(int userId)
        {
            
            var all = (
                from c in myContext.Cases
                join u in myContext.Users on c.UserId equals u.Id
                join p in myContext.Priorities on c.PriorityId equals p.Id
                join ct in myContext.Categories on c.CategoryId equals ct.Id
                
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    Level = c.Level,
                    UserId = u.Id,
                    UserName = u.Name,
                    PriorityName = p.Name,
                    CategoryName = ct.Name
                    
                }).ToList();
            return all.Where(x => x.UserId == userId && (x.Review != null || x.Review > 0));
        }

        //by staff------------------------------------------------------------------------------------------------------------
        /*public IEnumerable<CaseVM> ViewTicketsByStaffId(int staffId)
        {
            //var history = myContext.Histories.OrderByDescending(e => e.DateTime).Where(u => u.UserId == userId).Select(c => c.CaseId);
            *//*var staff = myContext.StaffCases.Where(sc => sc.StaffId == staffId).Select(c => c.CaseId);*//*
            var all = (
                from c in myContext.Cases
                join u in myContext.Users on c.UserId equals u.Id
                join p in myContext.Priorities on c.PriorityId equals p.Id
                join ct in myContext.Categories on c.CategoryId equals ct.Id
              
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    Level = c.Level,
                    UserId = u.Id,
                    UserName = u.Name,
                    PriorityName = p.Name,
                    CategoryName = ct.Name,
                    
                }).ToList();
            *//*return all.Where(s => staff.Contains(s.Id) && s.EndDateTime == null);*//*
        }*/
        //history by staff----------------------------------------------------------------------------------------------
       /* public IEnumerable<CaseVM> ViewHistoryTicketsByStaffId(int staffId)
        {
            //var history = myContext.Histories.OrderByDescending(e => e.DateTime).Where(u => u.UserId == userId).Select(c => c.CaseId);
           *//* var staff = myContext.StaffCases.Where(sc => sc.StaffId == staffId).Select(c => c.CaseId);*//*
            var all = (
                from c in myContext.Cases
                join u in myContext.Users on c.UserId equals u.Id
                join p in myContext.Priorities on c.PriorityId equals p.Id
                join ct in myContext.Categories on c.CategoryId equals ct.Id
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    Level = c.Level,
                    UserId = u.Id,
                    UserName = u.Name,
                    PriorityName = p.Name,
                    CategoryName = ct.Name
                    
                }).ToList();
            *//*return all.Where(s => staff.Contains(s.Id) && s.EndDateTime != null);*//*
        }*/

        //View ticket by difficulty level------------------------------------------------------------------------
        public IEnumerable<CaseVM> ViewTicketByLevel(int level) {
            var all = (
                    from c in myContext.Cases
                    join u in myContext.Users on c.UserId equals u.Id
                    join p in myContext.Priorities on c.PriorityId equals p.Id
                    join ct in myContext.Categories on c.CategoryId equals ct.Id
                    /*join scs in myContext.StaffCases on c.Id equals scs.CaseId
                    join s in myContext.Staffs on scs.StaffId equals s.Id*/
                    select new CaseVM
                    {
                        Id = c.Id,
                        Description = c.Description,
                        StartDateTime = c.StartDateTime,
                        EndDateTime = c.EndDateTime,
                        Review = c.Review,
                        Level = c.Level,
                        UserId = u.Id,
                        UserName = u.Name,
                        PriorityName = p.Name,
                        CategoryName = ct.Name,
                        /*StaffId = s.Id*/
                    }
                ).ToList();
            return all.Where(e => e.EndDateTime == null && e.Level == level && (e.StaffId != null || e.StaffId > 0)).OrderByDescending(s => s.StartDateTime);
        }

        public int NextLevel(int caseId) {
            int result = 0;

            var history = myContext.Histories.OrderByDescending(e => e.DateTime).FirstOrDefault(c => c.CaseId == caseId);

            if (history == null) {
                return 0;
            }
            
            var cases = myContext.Cases.Find(caseId);
            if (cases == null) {
                return 0;
            }

           /* var staff = myContext.StaffCases.FirstOrDefault(s => s.CaseId == caseId);*/
            //Mendapatkan staff
            if (history.Level <= 2) {
                cases.Level = cases.Level + 1;
               /* staff.StaffId = 1;*/
                myContext.Cases.Update(cases);
                /*myContext.StaffCases.Update(staff);*/
                result = myContext.SaveChanges();

                var histories = new History()
                {
                    DateTime = DateTime.Now,
                    Level = history.Level+1,
                    //Description = $"[Staff] Staff id ({staff.StaffId}) want to help ({caseId} to next level ({history.Level + 1}))",
                    UserId = history.UserId,
                    CaseId = history.CaseId,
                    StatusCodeId = 2
                };
                myContext.Histories.Add(histories);
                result = myContext.SaveChanges();
                
            }
            return result;
        }

        public int ChangePriority(PriorityVM priorityVM) {
            var result = 0;

            var getCases = myContext.Cases.Find(priorityVM.CaseId);

            if (getCases == null) {
                return 0;
            }

            var history = new History()
            {
                DateTime = DateTime.Now,
                Description = $"(Staff) Staff id ({priorityVM.StaffId}) Change Priority of Case id ({priorityVM.CaseId}) from priority ({getCases.PriorityId}) to ({priorityVM.PriorityId})",
                UserId = priorityVM.UserId,
                Level = getCases.Level,
                CaseId = getCases.Id,
                StatusCodeId = 2
            };
            //var getStaff = myContext.StaffCases.FirstOrDefault(s => s.CaseId == priorityVM.CaseId);
            //getStaff.StaffId = priorityVM.StaffId;
            //myContext.StaffCases.Update(getStaff);
            result = myContext.SaveChanges();

            getCases.PriorityId = priorityVM.PriorityId;
            myContext.Cases.Update(getCases);
            result = myContext.SaveChanges();

            myContext.Histories.Add(history);
            result = myContext.SaveChanges();

            return result;
        }

        public int HandleTicket(CloseTicketVM closeTicket) {
            var result = 0;

            var getHistory = myContext.Histories.OrderByDescending(h => h.DateTime)
                .FirstOrDefault(c => c.CaseId == closeTicket.CaseId);
            
            if (getHistory.Level <= 2) {
                

                var history = new History()
                {
                    DateTime = DateTime.Now,
                    Description = $"(Staff) ({closeTicket.StaffId}) Handling CaseId ({closeTicket.CaseId})",
                    Level = getHistory.Level,
                    UserId = getHistory.UserId,
                    CaseId = getHistory.CaseId,
                    StatusCodeId = 2
                };
                //var getStaff = myContext.StaffCases.FirstOrDefault(s => s.CaseId == closeTicket.CaseId);
                //var getCases = myContext.Cases.Find(closeTicket.CaseId);
                //getStaff.StaffId = closeTicket.StaffId;

               // myContext.StaffCases.Update(getStaff);
                result = myContext.SaveChanges();

                myContext.Histories.Add(history);
                result = myContext.SaveChanges();
            }
            return result;
        }

        public int CloseTicketById(CloseTicketVM closeTicketVM)
        {
            var result = 0;

            var cases = myContext.Cases.Find(closeTicketVM.CaseId);
            if (cases != null)
            {
                cases.EndDateTime = DateTime.Now;
                myContext.Cases.Update(cases);
                myContext.SaveChanges();

                var lastHistory = myContext.Histories.OrderByDescending(d => d.DateTime).FirstOrDefault(h => h.CaseId == closeTicketVM.CaseId);
                History history = new History()
                {
                    CaseId = cases.Id,
                    Description = $"(System) Closed Ticket By Staff ({closeTicketVM.StaffId})",
                    DateTime = DateTime.Now,
                    Level = lastHistory.Level,
                    UserId = closeTicketVM.UserId,
                    StatusCodeId = 3
                };
               // var getStaff = myContext.StaffCases.FirstOrDefault(s => s.CaseId == closeTicketVM.CaseId);
                //getStaff.StaffId = closeTicketVM.StaffId;
               // myContext.StaffCases.Update(getStaff);
                result = myContext.SaveChanges();

                myContext.Histories.Add(history);
                result = myContext.SaveChanges();
                return result;
            }
            else {
                return 0;
            }
        }

        public int ReviewTicket(ReviewVM review) {
            var result = 0;
            var getCase = myContext.Cases.Find(review.CaseId);

            if (getCase != null)
            {

                getCase.Review = review.Review;
                myContext.Cases.Update(getCase);
                result = myContext.SaveChanges();

                var lastHistory = myContext.Histories.OrderByDescending(e => e.DateTime).FirstOrDefault();
                History history = new History()
                {
                    CaseId = getCase.Id,
                    Description = $"(Client) Ticket ({getCase.Id} Review by client ({review.UserId}))",
                    DateTime = DateTime.Now,
                    Level = lastHistory.Level,
                    UserId = review.UserId,
                    StatusCodeId = lastHistory.StatusCodeId
                };
                myContext.Histories.Add(history);
                result = myContext.SaveChanges();
                return result;
            }
            else {
                return 0;
            }
        }
    }
}
