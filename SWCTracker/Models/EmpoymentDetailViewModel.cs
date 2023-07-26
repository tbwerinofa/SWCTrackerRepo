using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class EmploymentDetailViewModel
    {
        public int AddressId { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "company name is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string? Name { get; set; }


        [Display(Name = "Contact Person")]
        [Required(ErrorMessage = "contact person required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string? Contact { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Work Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string? WorkTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cell")]
        [Required(ErrorMessage = "contact number required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string? CellPhone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        public string? Email { get; set; }

        [DisplayName("Started")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }

        [DisplayName("Date Left")]
        [DataType(DataType.Date)]
        public DateTime LeftDate { get; set; }
        public string? StartDateLongDate { get; set; }

        [Display(Name = "Salary/Wage (per hour)")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public decimal Salary { get; set; }


        [Display(Name = "Working Hours")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int WorkingHours { get; set; }

        [Display(Name = "Days per week")]
        [Required, Range(1, 7, ErrorMessage = "required")]
        public int DaysPerWeek { get; set; }


        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        public string? Duties { get; set; }

        [Display(Name = "Occupation")]
        [Required(ErrorMessage = "required")]
        public IEnumerable<int>? OccupationIds { get; set; }
        //public AddressViewModel? NextOfKinAddress { get; set; }

        public IEnumerable<DropDownListItem>? Occupations { get; set; }

        public AddressViewModel? Address { get; set; }


    }
}
