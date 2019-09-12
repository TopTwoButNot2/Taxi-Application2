using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class BusinessLogic
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void CreatePassenger(string firstname, string lname, string email, string phone, string DepertureDate)
        {
            Passenger pas = new Passenger
            {
                PassengerId = Guid.NewGuid().ToString(),
                FirstName = firstname,
                LastName = lname,
                EmailAddress = email,
                PhoneNumber = phone,
                DepertureDate=DepertureDate

            };

            db.Passengers.Add(pas);
            db.SaveChanges();
        }

        public List<Route> getRoutes()
        {
            return db.Routes.ToList();
        }
        public List<Owner> GetOwners()
        {
            return db.Owners.ToList();
        }
        public List<Available> getAvailable()
        {
            return db.Availables.ToList();
        }
        public List<Rank> getRank()
        {
            return db.Ranks.ToList();
        }

        public List<Route> getRoute(int rid)
        {
            return db.Routes.ToList().FindAll(x=>x.RouteId==rid);
        }
        public List<Schedule> getRouteDetails(int rid)
        {
            return db.Schedules.ToList().FindAll(x => x.RouteId == rid);
        }
        
        public List<TaxiPosition> passengerAvail(int rid)
        {
            return db.TaxiPosition.ToList().FindAll(x => x.schedule.RouteId == rid);
        }
        //public List<Laggage> laggage(int rid)
        //{
        //    return db.laggagees.ToList().FindAll(x => x.schedule.RouteId == rid);
        //}

        public List<StopOver> StopOvers(int rid)
        {
            return db.StopOvers.ToList().FindAll(x => x.RouteId == rid);
        }
       
        public bool Exists(string email)
        {
            bool exist = false;
            Passenger pas = db.Passengers.ToList().Find(x => x.EmailAddress == email);
            if (pas != null)
            {
                exist = true;
            }

            Owner owner = db.Owners.ToList().Find(x => x.Email == email);
            if (owner != null)
            {
                exist = true;
            }
            Driver driver = db.Drivers.ToList().Find(x => x.Email == email);
            if (driver != null)
            {
                exist = true;
            }
            RankManager rankManager = db.RankManagers.ToList().Find(x => x.Email == email);
            if (rankManager != null)
            {
                exist = true;
            }

            return exist;
        }

        public string Admin(string username)
        {
            try
            {
                return db.Admins.ToList().Find(x => x.Email == username).FirstName;

            }
            catch (Exception)
            {
                return "";
            }
        }
        public string Driver(string username)
        {
            try
            {
                return db.Drivers.ToList().Find(x => x.Email == username).FirstName;

            }
            catch (Exception)
            {
                return "";
            }
        }
        public string Owner(string username)
        {
            try
            {
                return db.Owners.ToList().Find(x => x.Email == username).FirstName;

            }
            catch (Exception)
            {
                return "";
            }
        }
        public string RankManager(string username)
        {
            try
            {
                return db.RankManagers.ToList().Find(x => x.Email == username).FirstName;

            }
            catch (Exception)
            {
                return "";
            }
        }
        public string Passenger(string username)
        {
            try
            {
                return db.Passengers.ToList().Find(x => x.EmailAddress == username).FirstName;

            }
            catch (Exception)
            {
                return "";
            }
        }
        public string Dashboard(string email)
        {
            string dashboard = "Error";

            try
            {
                Admin ad = db.Admins.ToList().Find(x => x.Email == email);
                if (ad != null)
                {
                    dashboard = "Admin";
                }

                RankManager rm = db.RankManagers.ToList().Find(x => x.Email == email);
                if (rm != null)
                {
                    dashboard = "RankManager";
                }

                Owner ow = db.Owners.ToList().Find(x => x.Email == email);
                if (ow != null)
                {
                    dashboard = "Owner";
                }

                Driver dr = db.Drivers.ToList().Find(x => x.Email == email);
                if (dr != null)
                {
                    dashboard = "Driver";
                }

                Passenger pas = db.Passengers.ToList().Find(x => x.EmailAddress == email);
                if (pas != null)
                {
                    dashboard = "Passenger";
                }
            }
            catch (Exception)
            {
                return dashboard;
            }

            return dashboard;

        }

        public List<Owner> Owners()
        {
            return db.Owners.ToList();
        }

        //public TaxiModel TM(string tmid)
        //{
        //    return db.Models.ToList().Find(x => x.ModeId == tmid);
        //}

        //public Taxi getByPosition(int pos)
        //{
        //    try
        //    {
        //        string tp = db.TaxiPosition.ToList().Find(x => x.Position == pos).TaxiNo;

        //        Taxi tx = db.Taxis.ToList().Find(x => x.TaxiNo == tp);
        //        return tx;
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return null;
            
        //}

        public List<Route> AllRoutes()
        {
            return db.Routes.ToList();
        }

        public TaxiMake TaxiMake(string tmid)
        {
            return db.TaxiMakes.ToList().Find(x => x.MakeId == tmid);
        }

        //public List<Taxi> OwnerTaxis(string email)
        //{
        //    var owner = db.Owners.ToList().Find(x => x.Email == email);
        //    return db.Taxis.ToList().FindAll(x => x.ownerID == owner.ownerID);
        //}
    }
}