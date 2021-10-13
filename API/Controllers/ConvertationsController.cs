using API.Base;
using API.Model;
using API.Repository.Data;
using API.ViewModel;
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
    public class ConvertationsController : BaseController<Convertation, ConvertationRepository, int>
    {
        private readonly ConvertationRepository convertationRepository;

        public ConvertationsController(ConvertationRepository convertationRepository) : base(convertationRepository)
        {
            this.convertationRepository = convertationRepository;
        }

        [HttpPost("CreateConvertations")]
        public ActionResult CreateConvertation(CreateConvertationVM createConvertationVM) {
            var create = convertationRepository.CreateConvertation(createConvertationVM);
            if (create > 0)
            {
                return Ok("Anda Berhasil memulai percakapan anda");
            }
            else {
                return BadRequest("Gagal membuat percakapan");
            }
        }

        [HttpGet("GetConvertations")]
        public ActionResult GetConvertations() {
            var get = convertationRepository.GetConvertations();
            if (get != null)
            {
                return Ok(get);
            }
            else {
                return BadRequest("Percakapan tidak ditemukan");
            }
        }

        [HttpGet("ViewConvertations")]
        public ActionResult ViewConvertations() {
            var getConvertation = convertationRepository.ViewConvertation();
            if (getConvertation != null)
            {
                return Ok(getConvertation);
            }
            else {
                return BadRequest("Percakapan Tidak ditemukan");
            }
        }

        [HttpGet("ViewConvertationCaseId/{id}")]
        public ActionResult ViewConvertationsByCaseId(int id)
        {
            var getConvertationByCaseId = convertationRepository.ViewConvertationCaseId(id);
            if (getConvertationByCaseId != null)
            {
                return Ok(getConvertationByCaseId);
            }
            else
            {
                return BadRequest("Percakapan dengan id kasus tersebut Tidak Ditemukan");
            }

        }

        [HttpGet("ViewConvertationUserId/{id}")]
        public ActionResult ViewConvertationsByUserId(int id)
        {
            var getUserId = convertationRepository.ViewConvertationUserId(id);
            if (getUserId != null)
            {
                return Ok(getUserId);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }
    }
}
