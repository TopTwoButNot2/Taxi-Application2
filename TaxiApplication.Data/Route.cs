using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
    public class Route
    {
        [Key]        
        public int RouteId { get; set; }
        [Required]
        [StringLength(maximumLength: 35, ErrorMessage = "Route Name must be at least 2 characters long", MinimumLength = 2)]
        public string RouteName { get; set; }

   
        public Nullable<double> RouteDistance { get; set; }
        public Nullable<double> RouteDuration { get; set; }
        public double latDurban { get; set; }
        public double longitudeDurban { get; set; }
        public double latDestination { get; set; }
        public double longitudeDestination { get; set; }


       
        public string StopOver { get; set; }
        public byte[] picture { get; set; }
        public string ImageType { get; set; }
        public bool isAvailable { get; set; }
        public string RankId { get; set; }
        public virtual Rank Rank { get; set; }
        public string rankmanagerID { get; set; }
        public virtual RankManager rankmanager { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        
        public ICollection<StopOver> StopOvers { get; set; }

        public ICollection<Price> prices { get; set; }



    }
}
