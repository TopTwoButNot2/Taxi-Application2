using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;


namespace TaxiApplication.BusinessLogics
{
    public class DriverLogics
    {
        private ApplicationDbContext db;

        public DriverLogics ()
        {
            this.db = new ApplicationDbContext();
            
        }

        public List<Driver> GetDrivers(string email)
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

        public bool AddDriver(Driver driver)
        {
            try
            {
                //driver.driverID = Guid.NewGuid().ToString();
                db.Drivers.Add(driver);
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
