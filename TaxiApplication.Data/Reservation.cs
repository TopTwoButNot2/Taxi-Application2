﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
    public class Reservation
    {
        [Key]
        public int BookingId { get; set; }
        public Nullable<int> NumberLaggagSmall { get; set; }
        public Nullable<int> NumberLaggageMed { get; set; }
        public Nullable<int> NumberLaggageLrg { get; set; }
        public string picture { get; set; }
        //public string Size { get; set; }
        public string status { get; set; }
        public decimal TotalPrice { get; set; }
        public string PassengerName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd - MM - yyyy}", ApplyFormatInEditMode = true)]
        public string ImageType { get; set; }
        public  int NumLaggages { get; set; }
        public string PassengerId { get; set; }
        [ForeignKey("PassengerId")]
        public Passenger passengers { get; set; }

        public int RouteId { get; set; }

        public int ID { get; set; }
      

        public virtual Schedule schedule { get; set; }
        public Nullable<int> No { get; set; }


        ApplicationDbContext db = new ApplicationDbContext();
        public List<LaggageSetting> setting()
        {
            return db.LaggageSettings.ToList();
        }
        public List<LaggagePrices> pricess()
        {
            return db.LaggagePrice.ToList();
        }

        public string SelectRoute()
        {
            var s = db.Routes.Where(n => n.RouteId == RouteId).Select(n => n.RouteName).FirstOrDefault();

            return s;
        }
        public string getRoutName()
        {
            var routName = (from r in db.Routes
                where r.RouteId == RouteId
                select (r.RouteName)).FirstOrDefault();
            return routName;
        }

        public Nullable<int> addLaggages()
        {
            return NumberLaggagSmall + NumberLaggageLrg + NumberLaggageMed;
        }
        //public string getTaxi()
        //{
        //    var routName = (from r in db.Routes
        //                    where r.RouteId == RouteId
        //                    select (r.Rank.)).FirstOrDefault();
        //    return routName;
        //}


        //public string getSize()
        //{
        //    string sizz = "";
        //    foreach (var item in setting())
        //    {
        //        if (NumberLaggage > item.Test && NumberLaggage <= item.NumOfBags)
        //        {
        //            sizz = item.Size;
        //        }

        //    }
        //    return sizz;
        //}

        //public decimal getPrice()
        //{
        //    decimal price = 0;
        //    foreach (var item in pricess())
        //    {
        //        if (getSize() == item.ls.Size)
        //        {
        //            price = item.price;
        //        }

        //    }
        //    return price;
        //}

        public string getDriverName()
        {
            var drivername = (from s in db.Schedules
                join o in db.Owners on s.ownerID equals o.ownerID
                join d in db.Drivers on o.ownerID equals d.ownerID
                select d.FirstName).FirstOrDefault();
            return drivername;

        }
        //public decimal price(int p)
        //{
        //    Price mk = db.Prices.ToList().Find(x => x.PriceID == p);
        //    return mk.pricevalue;
        //}
        //public string stopover(int s)
        //{
        //    StopOver mk = db.StopOvers.ToList().Find(x => x.StopID == s);
        //    return mk.stop;
        //}
        //public string taxii(string t)
        //{
        //    Taxi mk = db.Taxis.ToList().Find(x => x.TaxiNo == t);
        //    return mk.RegNo;
        //}

    }
}
