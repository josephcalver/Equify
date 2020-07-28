using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Equify.Models
{

    public class Deal
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public int Founded { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Country { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Region { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Sector { get; set; }

        [Display(Name = "Enterprise Fair Value")]
        [Range(1, 1000)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal EnterpriseValue { get; set; }

        [Display(Name = "Deal Type")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string DealType { get; set; }

        [Display(Name = "Deal Origination Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DealOriginationDate { get; set; }

        [Display(Name = "Deal Status")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string DealStatus { get; set; }

        [Display(Name = "Fund Investing")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string FundInvesting { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Currency { get; set; }

        [Display(Name = "Equity Required")]
        [Required]
        public int EquityRequired { get; set; }

        [Display(Name = "Deal Team Lead")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string DealTeamLead { get; set; }

        [Display(Name = "Investor Relations Lead")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string InvestorRelationsLead { get; set; }

        [Display(Name = "ESG Lead")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string ESGLead { get; set; }
    }
}