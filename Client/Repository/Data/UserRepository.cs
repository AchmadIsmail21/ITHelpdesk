using API.Model;
using API.ViewModel;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class UserRepository : GeneralRepository<User, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(Address address, string request = "Users/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<JWTokenVM> Auth(LoginVM login) {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login)
                , Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login", content);
            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);
            
            return token;
        }

        public async Task<List<User>> GetUsers() {
            List<User> data = new List<User>();

            using (var response = await httpClient.GetAsync(request)) {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<User>>(apiResponse);
            }
            return data;
        }

        public HttpStatusCode InsertUser(User user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request, content).Result;
            return result.StatusCode;
        }

        public async Task<RegisterVM> GetProfileById(int id) {
            RegisterVM register = null;

            using (var response = await httpClient.GetAsync(request + "GetProfileById/" + id)) {
                string apiResponse = await response.Content.ReadAsStringAsync();
                register = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);
            }
            return register;
        }

        public async Task<UserSessionVM> GetUserByEmail(string email)
        {
            UserSessionVM userSession = null;

            using (var response = await httpClient.GetAsync(request + "GetUserByEmail/" + email))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userSession = JsonConvert.DeserializeObject<UserSessionVM>(apiResponse);
            }
            return userSession;
        }
    }
}
