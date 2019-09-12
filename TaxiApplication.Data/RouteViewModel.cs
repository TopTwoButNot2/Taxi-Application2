using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Data
{
    public class RouteViewModel
    {
        [Key]
        public string rvmId { get; set; }
        public string RankName { get; set; }
        public string RouteName { get; set; }
        public string RouteStatus { get; set; }
        public double RouteDistance { get; set; }
        public double RouteDuration { get; set; }
        public decimal Pricevalue { get; set; }
        public string TaxiStatus { get; set; }
        public string StopOvers { get; set; }   
        public string Size { get; set; }
        public string DriverName { get; set; }
        public string MakeType { get; set; }
        public int NumberLaggage { get; set; }
        public double LaggagePrice { get; set; }
        public double TotalPrice { get; set; }


        public string RegNo { get; set; }
        public Schedule schedule {get;set;}      
        public Price price { get; set; }       
        //public Taxi taxi { get; set; }      
        public StopOver stopOver { get; set; }
    }
}