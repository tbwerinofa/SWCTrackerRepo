using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class CalculatorViewModel
    {
        public int Id { get; set; }
        public int SectorId { get; set; }
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Occupation")]
        public int OccupationId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Occupation Group")]
        public int OccupationGroupId { get; set; }

        [Display(Name = "Salary/Wage rate")]
        [Required, Range(1, 10000, ErrorMessage = "must be more than 10000")]
        public decimal Salary { get; set; }


        [Display(Name = "Working Hours")]
        [Required, Range(1, 24, ErrorMessage = "must be less than or equal to 24")]
        public int WorkingHours { get; set; }

        [Display(Name = "Days")]
        [Required, Range(1, 7, ErrorMessage = "must be less than or equal to 7")]
        public int DaysPerWeek { get; set; }

        public IEnumerable<SelectListItem>? OccupationGroups { get; set; }

        public IEnumerable<SelectListItem>? Occupations { get; set; }
        public bool IsPostBack { get; set; }
    }
}
