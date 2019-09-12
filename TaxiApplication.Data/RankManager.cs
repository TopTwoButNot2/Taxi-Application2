using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class RankManager
    {
        [Key]
        public string rankmanagerID { get; set; }
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "First Name must be at least 2 characters long", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Last Name must be at least 2 characters long", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DisplayName("Phone Number"), DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "e.g Username@gmail.com")]
        public string Email { get; set; }

        public virtual Rank Rank { get; set; }
        public string RankId { get; set; }

        public ICollection<Route> Routes { get; set; }

    }
}