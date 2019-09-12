using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class Driver
    {
        [Key]
        public string driverID { get; set; }
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "First Name must be at least 2 characters long", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "First Name must be at least 2 characters long", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [DisplayName("Phone Number"), DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string LicenseCode { get; set; }

        [DataType(DataType.Date)]
        public DateTime LicenseExpiryDate { get; set; } 

        [DataType(DataType.Date)]
        public DateTime PDP { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "e.g Username@gmail.com")]
        public string Email { get; set; }

        public Owner Owner { get; set; }
        public string ownerID { get; set; }
    }
}