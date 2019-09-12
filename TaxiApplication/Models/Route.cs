using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Models
{
    public class Route
    {
        [Key]
        
        public int RouteId { get; set; }
        [Required]
        //[Display(Name = "Route Name")]
        //[RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        //[StringLength(maximumLength: 35, ErrorMessage = "Route Name must be atleast 2 characters long", MinimumLength = 2)]
        public string RouteName { get; set; }
        [Required]
        public double RouteDistance { get; set; }

        [Required]
        public bool isAvailable { get; set; }

        public string RankId { get; set; }
        public virtual Rank Rank { get; set; }
        public string rankmanagerID { get; set; }
        public virtual RankManager rankmanager { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<Price> Prices { get; set; }
        public ICollection<StopOver> StopOvers { get; set; }



    }
}