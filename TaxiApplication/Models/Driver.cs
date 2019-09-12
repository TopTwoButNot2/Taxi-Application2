using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Models
{
    public class Driver
    {
        [Key]
        public string driverID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [DisplayName("Phone Number"), DataType(DataType.PhoneNumber)]
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string LicenseCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime LicenseExpiryDate { get; set; }
        [Required]
        public string Email { get; set; }

        //public Owner Owner { get; set; }
        public string ownerID { get; set; }
    }
}