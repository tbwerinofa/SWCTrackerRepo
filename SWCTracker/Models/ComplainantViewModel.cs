using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SWCTracker.Models
{
    public class ComplainantViewModel
    {
        public int AddressId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "firstname is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string? FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "surname required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string? Surname { get; set; }

        [Display(Name = "Other name")]
        public string OtherName { get; set; }

        [Display(Name = "ID Number")]
        [Required(ErrorMessage = "ID Number is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "must be equal to 13 characters")]
        public string? IDNumber { get; set; }

        public string? Initials { get; set; }
        public string? IDType { get; set; }

        [Display(Name = "Occupation")]
        [Required(ErrorMessage = "occupation is required")]
        public string? Occupation { get; set; }

        [Display(Name = "Company")]
        public string? Company { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Work Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string WorkTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Home Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string HomeTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cell")]
        [Required(ErrorMessage = "contact number required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string CellPhone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        public string? Email { get; set; }
        public AddressViewModel? Address { get; set; }

        [Display(Name = "Are you party to the Bargaining Council?")]
        public bool IsTradeUnionReferral { get; set; }

        [Display(Name = "TradeUnion")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "gender is required")]
        public int TradeUnionId { get; set; }


        [Display(Name = "Emergency Contact Detail person 2")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string? NextOfKinFirstName { get; set; }

        [Display(Name = "Next of Kin Surname")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string? NextOfKinSurname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "emergency contact number required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string? NextOfKinContactNo { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "emergency contact number required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string? NextOfKinContactFax { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        public string? NextOfKinEmail { get; set; }
        public AddressViewModel? NextOfKinAddress { get; set; }

        public IEnumerable<SelectListItem>? TradeUnions { get; set; }
        public IEnumerable<SelectListItem>? Titles { get; set; }
        public IEnumerable<SelectListItem>? Nationalities { get; set; }
        public IEnumerable<SelectListItem>? IDTypes { get; set; }

        public string? DocumentPath { get; set; }

    }
}
