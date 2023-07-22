using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class AddressViewModel
    {
        [Display(Name = "Line 1")]
        [Required(ErrorMessage = "address line 1 isrequired")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters.")]
        public string? Line1 { get; set; }

        [Display(Name = "Line 2")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters.")]
        public string? Line2 { get; set; }

        [Display(Name = "Post Code")]
        [StringLength(10, ErrorMessage = "must be less than 10 characters.")]
        public string? Code { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Province")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "Province is required")]
        public int ProvinceId { get; set; }

        [Display(Name = "City")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "City is required")]
        public int CityId { get; set; }

        [Display(Name = "Town")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "Town is required")]
        public int TownId { get; set; }

        [Display(Name = "Suburb (Code)")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "Suburb required")]
        public int SuburbId { get; set; }
        public string? Country { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? Suburb { get; set; }
        public IEnumerable<SelectListItem>? Countries { get; set; }

        public IEnumerable<SelectListItem>? Cities { get; set; }

        public IEnumerable<SelectListItem>? Provinces { get; set; }
        public IEnumerable<SelectListItem>? Towns { get; set; }
        public IEnumerable<SelectListItem>? Suburbs { get; set; }
    }
}