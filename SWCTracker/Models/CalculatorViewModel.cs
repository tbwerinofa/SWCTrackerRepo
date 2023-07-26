using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class CalculatorViewModel
    {
        public int Id { get; set; }
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Occupation")]
        public int OccupationId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Occupation Group")]
        public int OccupationGroupId { get; set; }

        [Display(Name = "Salary/Wage (per hour)")]
        [Required, Range(1, 10000, ErrorMessage = "required")]
        public decimal Salary { get; set; }


        [Display(Name = "Working Hours")]
        [Required, Range(1, 24, ErrorMessage = "required")]
        public int WorkingHours { get; set; }

        [Display(Name = "Days per week")]
        [Required, Range(1, 7, ErrorMessage = "required")]
        public int DaysPerWeek { get; set; }

        public IEnumerable<SelectListItem> OccupationGroups { get; set; }

        public IEnumerable<SelectListItem> Occupations { get; set; }
        public bool IsPostBack { get; internal set; }
        public WageRateDetailResultSet WageRate { get; set; }
    }
}
