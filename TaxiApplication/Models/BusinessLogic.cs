using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models
{
    public class BusinessLogic
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void CreatePassenger(string firstname, string lname, string email, string phone)
        {
            Passenger pas = new Passenger
            {
                PassengerId = Guid.NewGuid().ToString(),
                FirstName = firstname,
                LastName = lname,
                EmailAddress = email,
                PhoneNumber = phone
            };

            db.Passengers.Add(pas);
            db.SaveChanges();
        }

        public List<Route> getRoutes()
        {
            return db.Routes.ToList();
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
    }
}