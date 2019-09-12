using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class LaggageLogic
    {
        private ApplicationDbContext db;
        public LaggageLogic()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Laggage2> GetLaggages()
        {
            return db.laggage2.ToList();
        }
        public bool AddWalkin(Laggage2 laggage)
        {
            var seasnid = db.Seasons.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).Select(x => x.SeasonID).FirstOrDefault();

            var smallPrice = db.laggagePricce2.Where(x => x.SeasonID == seasnid).Select(x => x.Price).Min();
            var LargePrice = db.laggagePricce2.Where(x => x.SeasonID == seasnid).Select(x => x.Price).Max();

            var mediumPrice = db.laggagePricce2.Where(x => x.SeasonID == seasnid && x.Price > smallPrice && x.Price < LargePrice).Select(x => x.Price).FirstOrDefault();



            //var routeid = db.Routes.Where(x => x.RouteName == laggage.RouteName).Select(x => x.RouteId).FirstOrDefault();
            //var routeprice = db.Prices.Where(x => x.RouteId == routeid).Select(x => x.pricevalue).FirstOrDefault();




            //var driverId = db.TaxiDrivers.Where(x => x.TaxiNo == laggage.TaxiNo).Select(x => x.driverID).FirstOrDefault();

            //var driverNAme = db.Drivers.Where(x => x.driverID == driverId).Select(x => x.FirstName).FirstOrDefault();




            try
            {
                var s = laggage.CalcSmallPrice() * smallPrice;
                var m = laggage.CalcMediumPrice() * mediumPrice;
                var l = laggage.CalLargePrice() * LargePrice;


                laggage.TotalPrice = s + m + l;
                laggage.DepartureDate = DateTime.Now;
                laggage.Count = laggage.CalcCount();
                db.laggage2.Add(laggage);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

