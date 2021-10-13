using API.Model;
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
    public class StatusCodeRepository : GeneralRepository<StatusCode, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public StatusCodeRepository(Address address, string request = "StatusCodes/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<StatusCode>> GetStatusCodes() {
            List<StatusCode> statusCodes = new List<StatusCode>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                statusCodes = JsonConvert.DeserializeObject<List<StatusCode>>(apiResponse);
            }
            return statusCodes;

        }
    }
}
