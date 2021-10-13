using API.Model;
using API.ViewModel;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class CaseRepository : GeneralRepository<Case, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public CaseRepository(Address address, string request = "Cases/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<CaseVM>> GetCase() { 
            List<CaseVM> cases = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request)) {
                string apiResponse = await response.Content.ReadAsStringAsync();
                cases = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return cases;
        }

        public async Task<List<CaseVM>> GetTicketsByStaffId(int staffId)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewTicketsByStaffId/" + staffId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

        public async Task<List<CaseVM>> GetTicketsByUserId(int userId) {
            List<CaseVM> user = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewTicketsByUserId/" + userId)) {
                string apiResponse = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return user;
        }

        public async Task<List<CaseVM>> GetHistoryTicketsByUserId(int userId)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewHistoryTicketsByUserId/" + userId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }


        public async Task<List<CaseVM>> GetHistoryTicketsByStaffId(int staffId)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewHistoryTicketByStaffId/" + staffId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

        public async Task<List<CaseVM>> GetTicketsByLevel(int level)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewTicketByLevel/" + level))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

    }
}
