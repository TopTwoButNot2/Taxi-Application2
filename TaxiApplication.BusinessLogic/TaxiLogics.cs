using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace TaxiApplication.BusinessLogics
{
    public class TaxiLogics
    {
        private ApplicationDbContext db;
        public TaxiLogics()
        {
            this.db = new ApplicationDbContext();
        }


        public List<Taxi> GetTaxis(string email)
        {
            List<Taxi> list = new List<Taxi>();
            var owner = db.Owners.ToList().Find(x => x.Email == email);
            foreach (var e in db.Taxis)
            {
                if (e.ownerID == owner.ownerID)
                {
                    list.Add(e);
                }

            }

            return list;
        }
        public bool Add(Taxi taxi)
        {
            try
            {
                var taximake = db.TaxiMakes.Where(x => x.MakeId == taxi.MakeId).Select(x => x.MakeType).FirstOrDefault();
                var image = db.TaxiMakes.Where(x => x.MakeId == taxi.MakeId).Select(x => x.Image).FirstOrDefault();

                var taximodel = db.TaxiModels.Where(x => x.TaxiModelID == taxi.TaxiModelID).Select(x => x.TaxiModelName).FirstOrDefault();
                

                taxi.TaxiMaketype = taximake;
                taxi.TaxiModeltype = taximodel;
                taxi.Image = image;

                db.Taxis.Add(taxi);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool RemoveItem(Taxi taxi)
        {
            try
            {
                db.Taxis.Remove(taxi);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Taxi GetTaxi(int? TaxiNo)
        {
            return db.Taxis.Find(TaxiNo);
        }
    }
}