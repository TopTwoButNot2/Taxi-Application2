using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Models
{
    public class Owner
    {
        [Key]
        public string ownerID { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        [DisplayName("Phone Number"), DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [Required]

        public string Email { get; set; }

        public ICollection<Driver> Drivers { get; set; }
        public ICollection<Taxi> Taxis { get; set; }
        public ICollection<Schedule> Schedules { get; set; }

    }
}