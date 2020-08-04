using System.Linq;
using System.Threading.Tasks;
using Equify.Models;
using Equify.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index(string fundInvesting, string dealStatus, string region,
            string sector, string dealType)
        {
            var selectLists = await _equifyApiService.GetDeals();
            
            var fundsInvestingQuery = from d in selectLists
                                    orderby d.FundInvesting
                                    select d.FundInvesting;
            
            var dealStatusesQuery = from d in selectLists
                                    orderby d.DealStatus
                                    select d.DealStatus;

            var regionsQuery = from d in selectLists
                                    orderby d.Region
                                    select d.Region;

            var sectorsQuery = from d in selectLists
                                    orderby d.Sector
                                    select d.Sector;

            var dealTypesQuery = from d in selectLists
                                    orderby d.DealType
                                    select d.DealType;

            var deals = from d in selectLists
                        orderby d.DealOriginationDate descending
                        select d;

            var dealList = await _equifyApiService.GetDeals(fundInvesting, dealStatus, region,
                sector, dealType);

            var dealPipelineViewModel = new DealPipelineViewModel
            {
                FundsInvesting = new SelectList(fundsInvestingQuery.Distinct().ToList()),
                DealStatuses = new SelectList(dealStatusesQuery.Distinct().ToList()),
                Regions = new SelectList(regionsQuery.Distinct().ToList()),
                Sectors = new SelectList(sectorsQuery.Distinct().ToList()),
                DealTypes = new SelectList(dealTypesQuery.Distinct().ToList()),
                Deals = dealList
            };

            return View(dealPipelineViewModel);
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
            Deal deal = await _equifyApiService.GetDeal(id);

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

        // GET: /DealPipeline/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Deal deal = await _equifyApiService.GetDeal(id);

            if (deal == null)
            {
                return NotFound();
            }
            return View(deal);
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
