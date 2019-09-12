using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
   public class WalkinPassengerLogic
    {
        private ApplicationDbContext db;
        public WalkinPassengerLogic()
        {
            this.db = new ApplicationDbContext();
        }
        public List<WalkingPassenger> GetWalkingPassengers()
        {
            return db.walkinPassenger.ToList();
        }
        public bool AddWalkin(WalkingPassenger walkingPassenger)
        {
            var seasnid = db.Seasons.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).Select(x => x.SeasonID).FirstOrDefault();

            var smallPrice = db.laggagePricce2.Where(x => x.SeasonID == seasnid).Select(x => x.Price).Min();
            var LargePrice = db.laggagePricce2.Where(x => x.SeasonID == seasnid).Select(x => x.Price).Max();

            var mediumPrice = db.laggagePricce2.Where(x => x.SeasonID == seasnid && x.Price > smallPrice && x.Price < LargePrice).Select(x => x.Price).FirstOrDefault();



            var routeid = db.Routes.Where(x=>x.RouteName==walkingPassenger.RouteName).Select(x => x.RouteId).FirstOrDefault();
            var routeprice = db.Prices.Where(x => x.RouteId == routeid).Select(x => x.pricevalue).FirstOrDefault();




            var driverId = db.TaxiDrivers.Where(x => x.TaxiNo == walkingPassenger.TaxiNo).Select(x => x.driverID).FirstOrDefault();

            var driverNAme = db.Drivers.Where(x => x.driverID == driverId).Select(x => x.FirstName).FirstOrDefault();




            try
            {
               var s = walkingPassenger.CalcSmallPrice() * smallPrice;
                var m = walkingPassenger.CalcMediumPrice() * mediumPrice;
                var l = walkingPassenger.CalLargePrice() * LargePrice;
                var R = routeprice;

                walkingPassenger.DriverName = driverNAme;
                walkingPassenger.TotalPrice = s + m + l + Convert.ToDouble(R);
                walkingPassenger.DepartureDate = DateTime.Now;
                walkingPassenger.Count = walkingPassenger.CalcCount();
                db.walkinPassenger.Add(walkingPassenger);
                db.SaveChanges();
                return true;
            }
           catch(Exception ex)
            {
                return false;
            }
        }
        
    }
}
