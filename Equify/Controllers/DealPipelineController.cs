using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Equify.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Equify.Controllers
{
    public class DealPipelineController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Deal> dealList = new List<Deal>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:8888/api/dealpipeline"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dealList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Deal>>(apiResponse);
                }
            }

                return View(dealList);
        }
    }
}
