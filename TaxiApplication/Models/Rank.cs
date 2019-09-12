using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Models
{
    public class Rank
    {
        [Key]
        public string RankId { get; set; }
        [Required]

        [Display(Name = "Rank Name")]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Rank Name must be atleast 2 characters long", MinimumLength = 2)]
        public string RankName { get; set; }

        [Display(Name ="Address or Location")]
        public string Location { get; set; }

        [Display(Name = "Latitude")]
        public string Lat { get; set; }

        [Display(Name = "Longitude")]
        public string Long { get; set; }

        public ICollection<RankManager> RankManagers { get; set; }
        public ICollection<Route> Routes { get; set; }
        public ICollection<Schedule> Schedules { get; set; }


    }
}