using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
   public class TaxiMakeLogics
    {
        private ApplicationDbContext db;
        public TaxiMakeLogics()
        {
            this.db = new ApplicationDbContext();
        }
        public List<TaxiMake> GetTaxiMakes()
        {
            return db.TaxiMakes.ToList();
        }
        public bool AddTaxiMake(TaxiMake taxiMake)
        {
            try
            {
                taxiMake.MakeId = Guid.NewGuid().ToString();
                db.TaxiMakes.Add(taxiMake);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool UpdateTaxiMake(TaxiMake taxiMake)
        {
            try
            {
                db.Entry(taxiMake).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveTaxiMake(TaxiMake taxiMake)
        {
            try
            {
                db.TaxiMakes.Remove(taxiMake);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public TaxiMake GetTaxiMake(int? taxi_Id)
        {
            return db.TaxiMakes.Find(taxi_Id);
        }
    }
}
