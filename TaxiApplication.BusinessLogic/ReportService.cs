using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
   public class ReportService
    {
        private ApplicationDbContext db;
        public ReportService()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Report> GetReports()
        {
            return db.Reports.ToList();
        }
    }
}
