using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class AvailableLogic
    {
        private ApplicationDbContext db;
        public AvailableLogic()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Available> GetAvailables()
        {
            return db.Availables.ToList();
        }
        public EnrouteDriver GetMapDetails(string taxiId)
        {
            var enroute = new EnrouteDriver()
            {
                rank = new Rank(),
                taxi = new Taxi()
            };
            try
            {
                var taxiDriver = db.TaxiDrivers.FirstOrDefault(x => x.TaxiNo == taxiId);
                if (taxiDriver != null)
                {
                    enroute.taxi = taxiDriver.taxi;
                    enroute.driverId = taxiDriver.driverID;
                    enroute.rank = taxiDriver.owner.rank;
                }
            }
            catch { }

            return enroute;
        }
        public bool Reserve(Available avail)
        {
            Reserved reserve = new Reserved();


            var po = db.reserved.Where(n => n.TaxiDriver.TaxiNo == avail.TaxiDriver.TaxiNo).Select(n => n.Count).FirstOrDefault();
            if (po > 0)
            {
                po = db.reserved.Where(n => n.TaxiDriver.TaxiNo == avail.TaxiDriver.TaxiNo).Select(n => n.Count).Max();
            }
            else
            {
                po = 0;
            }
            var max = db.reserved.Where(n => n.TaxiDriver.TaxiNo == avail.TaxiDriver.TaxiNo).Select(n => n.Count).Max();
            try
            {


                reserve.ID = avail.ID;
                reserve.td = avail.td;
                reserve.No = avail.No;
                reserve.DriverName = avail.DriverName;
                reserve.Week = avail.Week;
                reserve.Count = max + 1;

                avail.ReservedSeats = avail.NumSeats;
                db.reserved.Add(reserve);
                //data.available.Remove(avail);
                db.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Available GetAvailable(int? ID)
        {
            return db.Availables.Find(ID);
        }
        public bool CheckOut(Available avail)
        {
            Report av = new Report();
        

            try
            {
                av.ID = avail.ID;
                av.td = avail.td;
                av.No = avail.No;
                av.DriverName = avail.DriverName;
                av.Week = avail.Week;
                db.Reports.Add(av);
                //data.available.Remove(avail);
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
