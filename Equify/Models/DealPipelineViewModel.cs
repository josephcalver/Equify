using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equify.Models
{
    public class DealPipelineViewModel
    {
        public IEnumerable<Deal> Deals { get; set; }
        public SelectList FundsInvesting { get; set; }
        public SelectList DealStatuses { get; set; }
        public SelectList Regions { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList DealTypes { get; set; }
        public string FundInvesting { get; set; }
        public string DealStatus { get; set; }
        public string Region { get; set; }
        public string Sector { get; set; }
        public string DealType { get; set; }
    }
}
