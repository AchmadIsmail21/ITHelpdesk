using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : BaseController<History, HistoryRepository, int>
    {
        private readonly HistoryRepository historyRepository;
        public HistoriesController(HistoryRepository historyRepository) : base(historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        [HttpGet("GetHistory")]
        public ActionResult GetHistory() {
            var getHistory = historyRepository.GetHistory();

            if (getHistory != null)
            {
                return Ok(getHistory);
            }
            else {
                return BadRequest("History tidak ditemukan");
            }
        }
    }
}
