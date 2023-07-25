using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class OrganisationViewModel
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

        public AddressViewModel? Address { get; set; }

    }
}
