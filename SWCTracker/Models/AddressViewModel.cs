using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Address Line 1")]
        [Required(ErrorMessage = "address line 1 is required")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters.")]
        public string? Line1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters.")]
        public string? Line2 { get; set; }

        [Display(Name = "Post Code")]
        [StringLength(10, ErrorMessage = "must be less than 10 characters.")]
        public string? Code { get; set; }

        [Display(Name = "City")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "City is required")]
        public int CityId { get; set; }

        public string? City { get; set; }

        [Display(Name = "Suburb")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        [Required(ErrorMessage = "suburb is required")]
        public string? Suburb { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }

    }
}