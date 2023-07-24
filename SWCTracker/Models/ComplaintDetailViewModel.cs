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
        [Display(Name = "Are you party to the Bargaining Council?")]
        public bool ProcessCompliant { get; set; }


        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Display(Name = "Describe the process followed")]
        public string? ProcessDescription { get; set; }


        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Display(Name = "Summary nature of complaint")]
        [Required(ErrorMessage = "required")]
        public string? Summary { get; set; }

        [Display(Name = "Is Postal Address same as Personal Address?")]
        public bool IsSameAdress { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }

        public string? StartDateLongDate { get; set; }

        [Display(Name = "Salary/Wage")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public decimal Salary { get; set; }
        public int Left { get; set; }
        [Display(Name = "Working Hours")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int WorkingHours { get; set; }

        [Display(Name = "Days per week")]
        [Required, Range(1, 7, ErrorMessage = "required")]
        public decimal DaysPerWeek { get; set; }

        [Display(Name = "Occupations")]
        [Required(ErrorMessage = "required")]
        public int OccupationId { get; set; }

        [Display(Name = "Is Postal Address same as Personal Address?")]
        public bool IsConfirmed { get; set; }

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
