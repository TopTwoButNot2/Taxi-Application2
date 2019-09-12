using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Models
{
    public class RouteViewModel
    {
        [Key]
        public string rvmId { get; set; }
        public string RankName { get; set; }
        public string RouteName { get; set; }
        public double RouteDistance { get; set; }
        public string status { get; set; }
        public string RankId { get; set; }
        public int RouteId { get; set; }

    }
}