using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class ComplaintDetailViewModel
    {
        [Display(Name = "Capturer Full Name")]
        [Required(ErrorMessage = "capturer full name is required is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string? Capturer { get; set; }
        [Display(Name = "Have all internal grievance procedures been followed and exhausted? (If not, a full explanation and reason must be stated)?")]
        public bool ProcessCompliant { get; set; }


        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Display(Name = "Describe the process followed")]
        public string? ProcessDescription { get; set; }


        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Display(Name = "Summary nature of complaint")]
        [Required(ErrorMessage = "required")]
        public string? Summary { get; set; }

        [Display(Name = "Sector")]
        [Required(ErrorMessage = "required")]
        public IEnumerable<int>? SectorIds { get; set; }

        [Display(Name = "Nature of Complaint")]
        [Required(ErrorMessage = "required")]
        public IEnumerable<int>? ComplaintTypeIds { get; set; }

        public IEnumerable<SelectListItem>? Sectors { get; set; }
        public IEnumerable<SelectListItem>? ComplaintTypes { get; set; }
    }
}
