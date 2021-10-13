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
    public class ConvertationRepository : GeneralRepository<Convertation, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public ConvertationRepository(Address address, string request = "Convertations/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<Convertation>> GetConvertations() {
            List<Convertation> convertations = new List<Convertation>();

            using (var response = await httpClient.GetAsync(request)) {
                string apiResponse = await response.Content.ReadAsStringAsync();
                convertations = JsonConvert.DeserializeObject<List<Convertation>>(apiResponse);
            }
            return convertations;
        }

        public async Task<Convertation> GetConvertationByCaseId(int id) {

            Convertation convertation = null;

            using (var response = await httpClient.GetAsync(request + "ViewConvertationCaseId/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                convertation = JsonConvert.DeserializeObject<Convertation>(apiResponse);
            }
            return convertation;
        }
    }
}
