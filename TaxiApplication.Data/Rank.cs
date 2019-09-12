using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Data
{
    public class Rank
    {
        [Key] public string RankId { get; set; }

        [Required]
        [Display(Name = "Rank Name")]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Rank Name must be at least 2 characters long",
            MinimumLength = 2)]
        public string RankName { get; set; }

        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Province Name must be at least 2 characters long",
            MinimumLength = 2)]
        public string Province { get; set; }

        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "City Name must be at least 2 characters long",
            MinimumLength = 2)]
        public string City { get; set; }

        [DataType(DataType.Time)] public string OpenTime { get; set; }

        [DataType(DataType.Time)] public string CloseTime { get; set; }

        [Display(Name = "Address or Location")]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Location Name must be at least 2 characters long",
            MinimumLength = 2)]
        public string Location { get; set; }

        [Display(Name = "Latitude")] public string Lat { get; set; }

        [Display(Name = "Longitude")] public string Long { get; set; }

        public ICollection<RankManager> RankManagers { get; set; }
        public ICollection<Route> Routes { get; set; }
        public ICollection<Schedule> Schedules { get; set; }


    }
}
//}