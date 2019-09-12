using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class ReservedServices
    {
        private ApplicationDbContext db;
        public ReservedServices()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Reserved> GetReserved()
        {
            return db.reserved.ToList();
        }
    }
}
