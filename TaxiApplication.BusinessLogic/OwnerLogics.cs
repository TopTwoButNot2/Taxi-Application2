using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
   public class OwnerLogics
    {
        private ApplicationDbContext db;
        public OwnerLogics()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Owner>GetOwners()
        {
            return db.Owners.ToList();
        }
        public bool Add(Owner owner)
        {
            try
            {
                db.Owners.Add(owner);
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
