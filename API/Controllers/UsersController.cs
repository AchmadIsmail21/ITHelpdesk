using API.Base;
using API.Context;
using API.Model;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User, UserRepository, int>
    {
        //private readonly MyContext myContext;
        
        private readonly UserRepository userRepository;
        public UsersController(UserRepository userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM) {
            var register = userRepository.Register(registerVM);
            try {
                if (register == 100) {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Email Sudah Terdaftar"
                    });
                }
                else if(register == 200){
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Nomor Telepon Sudah Terdaftar"
                    });
                }
            } catch { 
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "Data Berhasil Di tambah"
            });
        }
        [EnableCors("AllowOrigin")]
        //[AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = userRepository.Login(loginVM);
            if (login == 404)
            {
                return BadRequest("Email tidak ditemukan, Silakan gunakan email lain");
            }
            else if (login == 401)
            {
                return BadRequest("Password salah");
            }
            else if (login == 1)
            {
                return Ok(
                    new JWTokenVM
                    {
                        Token = userRepository.GenerateTokenLogin(loginVM),
                        Messages = "Login Success"
                    }
                    );

            }
            else
            {
                return BadRequest("Gagal login");
            }
        }

        [HttpGet("GetUserByEmail/{email}")]
        public ActionResult GetUserByEmail(string email) {
            var get = userRepository.GetUserByEmail(email);

            if (get != null)
            {
                return Ok(get);
            }
            else {
                return BadRequest("Data dengan email tersebut tidak ditemukan");
            }
        }
        [Authorize]
        [HttpGet("GetClients")]
        public ActionResult GetClients() {
            var getAllClients = userRepository.GetClients();
            if (getAllClients != null)
            {
                return Ok(getAllClients);
            }
            else {
                return BadRequest("Data tidak ditemukan");
            }
        }
        [Authorize]
        [HttpGet("GetClientById/{id}")]
        public ActionResult GetClientById(int id) {
            var getById = userRepository.GetClientById(id);

            if (getById != null)
            {
                return Ok(getById);
            }
            else {
                return BadRequest("Data klien dengan id tersebut tidak ditemukan");
            }
        }

        [HttpGet("GetProfile")]
        public ActionResult GetProfile() {
            var get = userRepository.GetProfile();

            if (get != null)
            {
                return Ok(get);
            }
            else {
                return BadRequest("Data tidak ditemukan");
            }
        }

        [HttpPut("UpdateProfile")]
        public ActionResult UpdateProfile(User user) {
            var update = userRepository.UpdateProfile(user);
            if (update > 0)
            {
                return Ok("Data berhasil diubah");
            }
            else {
                return BadRequest("Data gagal diubah");
            }
        }
    }
}
