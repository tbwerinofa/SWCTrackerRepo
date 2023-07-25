﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string? OtherName { get; set; }

        [Display(Name = "ID Number")]
        [Required(ErrorMessage = "ID Number is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "must be equal to 13 characters")]
        public string? IDNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string? Telephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cell")]
        [Required(ErrorMessage = "contact number required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string? CellPhone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        public string? Email { get; set; }
        [Display(Name = "Next of Kin")]
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
        public AddressViewModel? Address { get; set; }
        public string FullName { get; set; }
    }
}
