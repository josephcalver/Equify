using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Equify.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Equify.Services
{
    public class EquifyApiService
    {

        public HttpClient Client { get; }

        public EquifyApiService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:8888/");
            Client = client;
        }

        public async Task<IEnumerable<Deal>> GetDeals()
        {
            var response = await Client.GetAsync("/api/dealpipeline");

            //response.EnsureSuccessStatusCode();

            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Deal>>(apiResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Deal>> GetDeal(int id)
        {
            var response = await Client.GetAsync($"/api/dealpipeline/{id}");

            Deal deal;

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                deal = JsonConvert.DeserializeObject<Deal>(apiResponse);
            }
            else
            {
                deal = null;
            }
            return deal;
        }

        public async Task<Deal> CreateDeal(Deal deal)
        {
            var dealJson = new StringContent(JsonConvert.SerializeObject(deal).ToString(),
                Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/api/dealpipeline", dealJson);

            Deal newDeal;

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                newDeal = JsonConvert.DeserializeObject<Deal>(apiResponse);
            }
            else
            {
                newDeal = null;
            }
            return newDeal;
        }

        [HttpPut]
        public async Task<ActionResult<Deal>> UpdateDeal(Deal deal)
        {
            var dealJson = new StringContent(JsonConvert.SerializeObject(deal).ToString(),
                Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"/api/dealpipeline", dealJson);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Deal>(apiResponse);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<Boolean> Delete(int id)
        {
            var response = await Client.DeleteAsync($"/api/dealpipeline/delete/{id}");
            return response.IsSuccessStatusCode;  
        }
    }
}

    
