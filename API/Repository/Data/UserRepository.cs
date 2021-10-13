using API.Context;
using API.Helper;
using API.Model;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UserRepository : GeneralRepository<MyContext, User, int>
    {
        private readonly MyContext myContext;
        private readonly DbSet<RegisterVM> entities;
        public IConfiguration Configuration;

        public UserRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<RegisterVM>();
            Configuration = configuration;
        }
        //Register
        public int Register(RegisterVM registerVM) {
            var hashPassword = HashGenerator.HashPassword(registerVM.Password);
            var result = 0;
            var checkEmail = myContext.Users.FirstOrDefault(u => u.Email == registerVM.Email);
            var checkPhone = myContext.Users.FirstOrDefault(u => u.Phone == registerVM.Phone);
            if (checkEmail == null && checkPhone == null)
            {
                User user = new User
                {
                    Name = registerVM.Name,
                    Email = registerVM.Email,
                    Password = hashPassword,
                    BirthDate = registerVM.BirthDate,
                    gender = (User.Gender)registerVM.gender,
                    RoleId = 1,
                    Phone = registerVM.Phone,
                    Address = registerVM.Address,
                    Department = registerVM.Department,
                    Company = registerVM.Company
                };
                myContext.Add(user);
                result = myContext.SaveChanges();

                return result;
            }
            else if (checkEmail != null)
            {
                return 100;
            }
            else if (checkPhone != null) {
                return 200;
            }
            return result;
        }
        //JwtLogin
        public string GenerateTokenLogin(LoginVM loginVM)
        {
            var user = myContext.Users.FirstOrDefault(u => u.Email == loginVM.Email);
            var ar = myContext.Roles.Single(r => r.Id == user.RoleId);
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Role",ar.Name)
                    
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //Loginn
        public int Login(LoginVM login)
        {
            var cek = myContext.Users.FirstOrDefault(u => u.Email == login.Email);
            var validatePassword = HashGenerator.ValidatePassword(login.Password, cek.Password);
            if (cek == null)
            {
                return 404;
            }

            if (validatePassword)
            {
                return 1;
            }
            else
            {
                return 401;
            }
        }
        //user sesi
        public UserSessionVM GetUserByEmail(string email) {
            var all = (from u in myContext.Users
                       join r in myContext.Roles
                       on u.RoleId equals r.Id
                       select new UserSessionVM { 
                            UserId = u.Id,
                            Name = u.Name,
                            Email = u.Email,
                            Role = r.Name,
                            RoleId = r.Id   
                       }
                       ).ToList();
            return all.FirstOrDefault(u => u.Email == email);
        }

        //Get all Data Client
        public IEnumerable<ProfileVM> GetClients() {
            //User user = new User();
            var all = (
                    from u in myContext.Users
                    join r in myContext.Roles
                    on u.RoleId equals r.Id
                    select new ProfileVM
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        BirthDate = u.BirthDate,
                        gender = (ProfileVM.Gender)u.gender,
                        RoleName = r.Name,
                        Phone = u.Phone,
                        Address = u.Address,
                        Department = u.Department,
                        Company = u.Company
                    }
                ).ToList();
            return all.Where(rn => rn.RoleName == "Client");
        }

        //get data client by Id
        public ProfileVM GetClientById(int id) {
            var all = (
                    from u in myContext.Users
                    join r in myContext.Roles
                    on u.RoleId equals r.Id
                    select new ProfileVM
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        BirthDate = u.BirthDate,
                        gender = (ProfileVM.Gender)u.gender,
                        RoleName = r.Name,
                        Phone = u.Phone,
                        Address = u.Address,
                        Department = u.Department,
                        Company = u.Company
                    }
                ).ToList();
            return all.Where(u => u.RoleName == "Client").FirstOrDefault(u => u.Id == id);
        }

        //profile
        public IEnumerable<ProfileVM> GetProfile() {
            var all = (
                    from u in myContext.Users
                    join r in myContext.Roles
                    on u.RoleId equals r.Id
                    select new ProfileVM
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        BirthDate = u.BirthDate,
                        gender = (ProfileVM.Gender)u.gender,
                        RoleName = r.Name,
                        Phone = u.Phone,
                        Address = u.Address,
                        Department = u.Department,
                        Company = u.Company
                    }
                ).ToList();
            return all;
        }

        //update profile
        public int UpdateProfile(User userUpdate) {
            var hashPassword = HashGenerator.HashPassword(userUpdate.Password);
            var result = 0;
            var user = myContext.Users.FirstOrDefault(u => u.Email == userUpdate.Email);
            if (user != null) {
                user.Email = userUpdate.Email;
                user.Name = userUpdate.Name;
                user.BirthDate = userUpdate.BirthDate;
                user.Phone = userUpdate.Phone;
                user.Address = userUpdate.Address;
                user.Department = userUpdate.Department;
                user.Company = userUpdate.Company;

                if (userUpdate.Password != "") {
                    user.Password = hashPassword;
                }
                myContext.Users.Update(user);
                result = myContext.SaveChanges();
            }
            return result;
        }
    }
}
