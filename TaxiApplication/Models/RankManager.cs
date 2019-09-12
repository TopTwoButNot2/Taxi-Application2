using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Models
{
    public class RankManager
    {
        [Key]
        public string rankmanagerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DisplayName("Phone Number"), DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }

        public virtual Rank Rank { get; set; }
        public string RankId { get; set; }

        public ICollection<Route> Routes { get; set; }

    }
}