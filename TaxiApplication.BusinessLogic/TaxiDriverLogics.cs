using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;

namespace TaxiApplication.BusinessLogics
{
    public class TaxiDriverLogics
    {
        private ApplicationDbContext db;

        public TaxiDriverLogics()
        {
            this.db = new ApplicationDbContext();

        }


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



        public List<TaxiDriver> GetTaxiDrivers(string email)
        {
            List<TaxiDriver> list = new List<TaxiDriver>();
            var owner = db.Owners.ToList().Find(x => x.Email == email);

            foreach(var e in db.TaxiDrivers)
            {
                if (e.ownerID == owner.ownerID)
                {
                    list.Add(e);
                }
            }
            return list;
        }
        public bool Add(TaxiDriver taxiDriver)
        {
            try
            {
             
                taxiDriver.NumSeats = taxiDriver.SelectSeats();
                db.TaxiDrivers.Add(taxiDriver);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(TaxiDriver taxiDriver)
        {
            try
            {
                db.TaxiDrivers.Remove(taxiDriver);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public TaxiDriver GetTaxiDriver(int? td)
        {
            return db.TaxiDrivers.Find(td);
        }

        public bool addPosition(string taxiNo, int no)
        {
            throw new NotImplementedException();
        }

        public void AddPosition(int position, string taxiNo, int no)
        {
            throw new NotImplementedException();
        }

        public object GetAvail(int? id)
        {
            throw new NotImplementedException();
        }

        public void AvailT(object v)
        {
            throw new NotImplementedException();
        }
        public List<TaxiDriver> TaxiDriversList(string email)
        {
            List<TaxiDriver> taxiDrivers = new List<TaxiDriver>();
            var owner = db.Owners.ToList().Find(n => n.Email == email);
            foreach (var x in db.TaxiDrivers)
            {
                if (x.ownerID == owner.ownerID)
                {
                    taxiDrivers.Add(x);
                }

            }
            return taxiDrivers;
        }
    }
}
