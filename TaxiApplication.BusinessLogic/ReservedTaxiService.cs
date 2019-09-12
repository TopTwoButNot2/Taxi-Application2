using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class ReservedTaxiService
    {
        private ApplicationDbContext db;
        public ReservedTaxiService()
        {
            this.db = new ApplicationDbContext();
        }
        public List<ReservedTaxi>GetReservedTaxisList()
        {
            return db.reservedTaxis.ToList();
        }
    }
}