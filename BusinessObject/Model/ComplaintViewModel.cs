using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BusinessObject
{
    public class ComplaintViewModel
    {
        public int Id { get; set; }
        public bool IsEmployeeRepresentative { get; set; }
        public bool IsBargainingCouncilParty { get; set; }
        public IEnumerable<int>? TradeUnionIds { get; set; }
        public string? SessionUserId { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "you must confirm to submit!")]
        public bool Agree { get; set; }
        public ComplainantViewModel? Employee { get; set; }

        public ComplainantViewModel? NextOfKin { get; set; }
        public EmploymentDetailViewModel? EmploymentDetail { get; set; }
        public ComplaintDetailViewModel? ComplaintDetail { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
        public IEnumerable<SelectListItem>? TradeUnions { get; set; }
    }
}
