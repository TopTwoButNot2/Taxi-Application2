using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Models
{
    public class NextOfKin
    {
        [Key]
        public string IdNo { get; set; }
        [Required]

        public string FName { get; set; }
        [Required]

        public string LName { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        [Required]

        public string Address { get; set; }
        public string PassengerId { get; set; }
        public virtual Passenger passenger { get; set; }
    }
}