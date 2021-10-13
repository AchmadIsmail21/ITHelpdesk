using Client.Base;
using API.Model;
using Client.Repository;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    /*[Authorize(Roles = "Client, Admin Support, IT Support")]*/
    /*[Route("[controller]")]*/
    [Authorize]
    public class DashBoardController : BaseController<User, UserRepository, int>
    {
        private readonly UserRepository userRepository;
        private readonly CaseRepository caseRepository;
        private readonly ConvertationRepository convertationRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly HistoryRepository historyRepository;
        private readonly PriorityRepository priorityRepository;
        private readonly RoleRepository roleRepository;
        
        private readonly StatusCodeRepository statusCodeRepository;

        public DashBoardController(UserRepository userRepository,
         CaseRepository caseRepository,
         ConvertationRepository convertationRepository,
         CategoryRepository categoryRepository,
         HistoryRepository historyRepository,
         PriorityRepository priorityRepository,
         RoleRepository roleRepository,
         StatusCodeRepository statusCodeRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.caseRepository = caseRepository;
            this.convertationRepository = convertationRepository;
            this.categoryRepository = categoryRepository;
            this.historyRepository = historyRepository;
            this.priorityRepository = priorityRepository;
            this.roleRepository = roleRepository;
            this.statusCodeRepository = statusCodeRepository;
        }

        public void GetSession() {
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.RoleId = HttpContext.Session.GetString("RoleId");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            

            ViewBag.CurrentPage = "";
           
            if (ViewBag.Role == "IT Support")
            {
                ViewBag.Level = 2;
            }
            else if (ViewBag.Role == "Admin Support")
            {
                ViewBag.Level = 1;
            }
            else
            {
                ViewBag.Level = 0;
            }
        }

        //Case Repository
        public async Task<JsonResult> GetCase() {
            GetSession();
            var cases = await caseRepository.GetCase();
            return Json(cases);
        }

        /*public async Task<JsonResult> GetHandleTickets(staffId) {
            
            if ( staffCase.StaffId != null)
            {
                var result = await caseRepository.GetTicketsByStaffId((int)staffCase.StaffId);
                return Json(result);
            }
            else {
                return Json(null);
            }
        }*/

        public async Task<JsonResult> GetTicketsUser() {
            GetSession();
            if (ViewBag.UserId != null)
            {
                var result = await caseRepository.GetTicketsByUserId(Int32.Parse(ViewBag.UserId));
                return Json(result);
            }
            else {
                return null;
            }
        }

        public async Task<JsonResult> GetHistoryTicketsUser() {
            GetSession();
            if (ViewBag.UserId != null)
            {
                var result = await caseRepository.GetHistoryTicketsByUserId(Int32.Parse(ViewBag.UserId));
                return Json(result);
            }
            else {
                return Json(null);
            }
        }

        public async Task<JsonResult> GetHistoryHandleTickets() {
            GetSession();
            if (ViewBag.StaffId != null)
            {
                var result = await caseRepository.GetHistoryTicketsByStaffId(Int32.Parse(ViewBag.StaffId));
                return Json(result);
            }
            else {
                return Json(null);
            }
        }

        public async Task<JsonResult> GetTicketsByLevel() {
            GetSession();
            if (ViewBag.Role != null)
            {
                var level = 0;
                if (ViewBag.Role == "IT Support")
                {
                    level = 2;
                }
                else if (ViewBag.Role == "Admin Support")
                {
                    level = 1;
                }
                else
                {
                    level = 0;
                }
                var result = await caseRepository.GetTicketsByLevel(level);
                return Json(result);
            }
            else {
                return Json(null);
            }
        }

        //Category Repo
        public async Task<JsonResult> GetCategories() {
            GetSession();
            var result = await categoryRepository.GetCategories();
            return Json(result);
        }

        //Convertation Repo
        public async Task<JsonResult> GetConvertations() {
            GetSession();
            var result = await convertationRepository.GetConvertations();
            return Json(result);
        }

        [HttpGet("{caseId}")]
        public async Task<IActionResult> GetConvertationsByCaseId(int caseId) {
            var result = await convertationRepository.GetConvertationByCaseId(caseId);
            return Json(result);
        }

        //History Repo
        public async Task<JsonResult> GetHistories() {
            GetSession();
            var result = await historyRepository.GetHistories();
            return Json(result);
        }

        //Priority Repo
        public async Task<JsonResult> GetPriorities() {
            GetSession();
            var result = await priorityRepository.GetPriorities();
            return Json(result);
        }

        //Role Repo
        public async Task<JsonResult> GetRoles() {
            GetSession();
            var result = await roleRepository.GetRoles();
            return Json(result);
        }

        //Staff Repo
        /*public async Task<JsonResult> GetStaffs() {
            GetSession();
            var result = await staffRepository.GetStaffs();
            return Json(result);
        }
        [HttpGet("{staffId}")]
        public async Task<IActionResult> GetStaffById(int staffId) {
            var result = await staffRepository.GetStaffById(staffId);
            return Json(result);
        }*/

        //StatusCode Repo
        public async Task<JsonResult> GetStatusCodes() {
            GetSession();
            var result = await statusCodeRepository.GetStatusCodes();
            return Json(result);
        }

        //User repo
        public async Task<JsonResult> GetUsers() {
            GetSession();
            var result = await userRepository.GetUsers();
            return Json(result);
        }

        //Dashboard
        
        public IActionResult Index()
        {
                GetSession();
                ViewBag.CurrentPage = "DashboardIndex";
                return View(); 
        }
        
        public IActionResult Tickets() {
            GetSession();
            ViewBag.CurrentPage = "Tickets";
            return View();
        }
        //[Authorize]
        public IActionResult HandleTickets() {
            GetSession();
            ViewBag.CurrentPage = "HandleTickets";
            return View();
        }

        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
