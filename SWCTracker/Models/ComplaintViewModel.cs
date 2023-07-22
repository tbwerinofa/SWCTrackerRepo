using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class ComplaintViewModel
    {
        [Display(Name = "Origin")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "title is required")]
        public int OriginId { get; set; }

        public PersonViewModel? Employee { get; set; }

        public PersonViewModel? NextOfKin { get; set; }
        public PersonViewModel? Employer { get; set; }
        public IEnumerable<SelectListItem>? Origins { get; set; }
    }
}
