using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class ComplaintViewModel
    {
        public int Id { get; set; }
        public bool IsEmployeeRepresentative { get; set; }
        public bool IsBargainingCouncilParty { get; set; }
        public IEnumerable<int>? TradeUnionIds { get; set; }
        public string? SessionUserId { get; set; }
        public ComplainantViewModel? Employee { get; set; }

        public ComplainantViewModel? NextOfKin { get; set; }
        public OrganisationViewModel? Organisation { get; set; }
        public EmploymentDetailViewModel? EmploymentDetail { get; set; }
        public ComplaintDetailViewModel? ComplaintDetail { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
        public IEnumerable<SelectListItem>? TradeUnions { get; set; }
    }
}
