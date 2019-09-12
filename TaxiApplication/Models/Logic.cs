using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models
{
    public class Logic
    {
        ApplicationDbContext db = new ApplicationDbContext();


        //public List<Driver>ListDrivers(string email)
        //{
        //    List<Driver> list = new List<Driver>();
        //    var owner = db.Owners.ToList().Find(x => x.Email == email);
        //    foreach (var e in db.Drivers)
        //    {
        //        if (e.ownerID == owner.ownerID)
        //        {
        //            list.Add(e);
        //        }

        //    }

        //    return list;
        //}

        public List<Taxi> TaxiList(string email)
        {
            List<Taxi> taxi = new List<Taxi>();
            var owner = db.Owners.ToList().Find(n => n.Email == email);
            foreach (var x in db.Taxis)
            {
                if (x.ownerID == owner.ownerID)
                {
                    taxi.Add(x);
                }

            }
            return taxi;

        }




        public List<Schedule> schedulList (string email)
        {
            List<Schedule> schedule = new List<Schedule>();
            var owner = db.Owners.ToList().Find(n => n.Email == email);
            foreach(var x in db.Schedules)
            {
                if(x.ownerID==owner.ownerID)
                {
                    schedule.Add(x);
                }

            }
            return schedule;

        }

        public void AddPosition(int position, string DriverId, string TaxiNo)
        {
            TaxiPosition tp = new TaxiPosition()
            {
                Position = position,
                TaxiNo = TaxiNo,
                driverID = DriverId
            };
            db.TaxiPosition.Add(tp);
            db.SaveChanges();
        }

        //public List<Route> getPosistion()
        //{
        //    List<Route> list = db.Routes.ToList();

        //    foreach (var e in db.Drivers)
        //    {
        //        foreach(var a in db.Routes)
        //        {
        //            if (e.Posetion ==a.Position)
        //            {
        //                list.Remove(a);
        //            }
        //        }
                
        //    }

        //    return list;
        //}

        //public bool checChe(string rout, int position)
        //{
        //    bool result = false;

        //    List<RouteOwnerVM> list = db.RouteOwnerVMs.ToList();

        //    foreach (var item in list)
        //    {
        //        if(item.Position==position || item.RouteId ==rout)
        //        {
        //            result = true;
        //        }
        //    }
        //    return result;

        //}
    }
}