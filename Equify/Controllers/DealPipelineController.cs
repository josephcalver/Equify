using System.Collections.Generic;
using System.Threading.Tasks;
using Equify.Models;
using Equify.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Equify.Controllers
{
    public class DealPipelineController : Controller
    {
        private readonly EquifyApiService _equifyApiService;

        public DealPipelineController(EquifyApiService equifyApiService)
        {
            _equifyApiService = equifyApiService;
        }

        // GET: /DealPipeline
        public async Task<IActionResult> Index()
        {
            IEnumerable<Deal> dealList = await _equifyApiService.GetDeals();
            return View(dealList);
        }

        // POST: /DealPipeline/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,Founded,Country,Region,Sector," +
            "DealType,DealOriginationDate,DealStatus,FundInvesting,Currency,EquityRequired,DealTeamLead," +
            "InvestorRelationsLead,ESGLead")] Deal deal)
        {
            ActionResult<Deal> newDeal = await _equifyApiService.CreateDeal(deal);

            if (newDeal == null)
            {
                return NotFound();
            }
            return View(newDeal);
        }

        // GET: /DealPipeline/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ActionResult<Deal> deal = await _equifyApiService.GetDeal(id);

            if (deal == null)
            {
                return NotFound();
            }
            return View(deal);
        }

        // POST: /DealPipeline/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,CompanyName,Founded,Country,Region,Sector," +
            "DealType,DealOriginationDate,DealStatus,FundInvesting,Currency,EquityRequired,DealTeamLead," +
            "InvestorRelationsLead,ESGLead")] Deal deal)
        {
            ActionResult<Deal> updatedDeal = await _equifyApiService.UpdateDeal(deal);

            if (updatedDeal == null)
            {
                return NotFound();
            }
            return View(updatedDeal);
        }

        // POST: /DealPipeline/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteSuccess = await _equifyApiService.Delete(id);

            if (!deleteSuccess)
            {
                NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
