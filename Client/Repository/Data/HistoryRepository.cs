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
    public class HistoryRepository : GeneralRepository<History, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public HistoryRepository(Address address, string request = "Histories/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<HistoryVM>> GetHistories() {
            List<HistoryVM> histories = new List<HistoryVM>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                histories = JsonConvert.DeserializeObject<List<HistoryVM>>(apiResponse);
            }
            return histories;
        }
    }
}
