using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class Logic
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public List<Driver> ListDrivers(string email)
        {
            List<Driver> list = new List<Driver>();
            var owner = db.Owners.ToList().Find(x => x.Email == email);
            foreach (var e in db.Drivers)
            {
                if (e.ownerID == owner.ownerID)
                {
                    list.Add(e);
                }

            }

            return list;
        }

        //public List<Taxi> TaxiList(string email)
        //{
        //    List<Taxi> taxi = new List<Taxi>();
        //    var owner = db.Owners.ToList().Find(n => n.Email == email);
        //    foreach (var x in db.Taxis)
        //    {
        //        if (x.ownerID == owner.ownerID)
        //        {
        //            taxi.Add(x);
        //        }

        //    }
        //    return taxi;

        //}




        public List<Schedule> schedulList(string email)
        {
            List<Schedule> schedule = new List<Schedule>();
            var owner = db.Owners.ToList().Find(n => n.Email == email);
            foreach (var x in db.Schedules)
            {
                if (x.ownerID == owner.ownerID)
                {
                    schedule.Add(x);
                }

            }
            return schedule;

        }

        //public void AddPosition(int position, string DriverId, string TaxiNo, int no)
        //{
        //    Taxi t = db.Taxis.ToList().Find(x => x.TaxiNo == TaxiNo);
        //    TaxiPosition tp = new TaxiPosition()
        //    {
        //        No = no,
        //        Position = position,
        //        TaxiNo = TaxiNo,
        //        driverID = DriverId


        //    };
        //    db.TaxiPosition.Add(tp);
        //    db.SaveChanges();
        //}

        //public bool addPosition(string DriverID, int No)
        //{
        //    TaxiPosition tp = db.TaxiPosition.ToList().Find(x => x.driverID == DriverID);
        //    Schedule s = db.Schedules.ToList().Find(x => x.No == No);

        //    if (tp != null && s != null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public List<Schedule> OwnerSchedules(string email)
        {
            var Owner = db.Owners.ToList().Find(x => x.Email == email);
            return db.Schedules.Where(x => x.ownerID == Owner.ownerID).ToList();
        }
        public TaxiPosition GetAvail(int? D)
        {
            return db.TaxiPosition.Find(D);
        }
        //public bool AvailT(TaxiPosition taxiPosition)
        //{

        //    try
        //    {

        //        Available avb = new Available();

        //        avb.ID = taxiPosition.ID;
        //        avb.No = taxiPosition.No;
        //        avb.Position = taxiPosition.Position;
        //        avb.TaxiNo = taxiPosition.TaxiNo;
        //        avb.driverID = taxiPosition.driverID;
        //        avb.Status = "ON the stand";



        //        // avb.Price = avb.SelectPrie();


        //        //if (get(taxiPosition.schedule.route.RouteName) == true)
        //        //{

        //        //    db.Entry(avb).State = System.Data.Entity.EntityState.Modified;
        //        //    db.SaveChanges();
        //        //}
        //        //else
        //        //{
        //        //    db.available.Add(avb);
        //        //    db.TaxiPosition.Remove(taxiPosition);
        //        //    db.SaveChanges();

        //        //}
        //        db.Availables.Add(avb);
        //        //db.TaxiPosition.Remove(taxiPosition);
        //        db.SaveChanges();


        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }


        //    //public List<Route> getPosistion()
        //    //{
        //    //    List<Route> list = db.Routes.ToList();

        //    //    foreach (var e in db.Drivers)
        //    //    {
        //    //        foreach(var a in db.Routes)
        //    //        {
        //    //            if (e.Posetion ==a.Position)
        //    //            {
        //    //                list.Remove(a);
        //    //            }
        //    //        }

        //    //    }

        //    //    return list;
        //    //}


        //}
        //public bool checChe(string rout, int position)
        //{
        //    bool result = false;

        //    List<Available> list = db.Availables.ToList();

        //    foreach (var item in list)
        //    {
        //        if (item.Position == position && item.schedule.route.RouteName == rout)
        //        {
        //            result = true;
        //        }
        //    }
        //    return result;
        //}




        public void PLeaseWork()
        {

        }
    }
}