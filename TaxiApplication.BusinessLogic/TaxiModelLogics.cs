using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
   public class TaxiModelLogics
    {
        private ApplicationDbContext db;
        public TaxiModelLogics()
        {
            this.db = new ApplicationDbContext();

        }
        public List<TaxiModel> GetTaxiModels()
        {
            return db.TaxiModels.ToList();
        }
        public bool Add(TaxiModel taxiModel)
        {
            try
            {
                taxiModel.TaxiModelID = Guid.NewGuid().ToString();
                db.TaxiModels.Add(taxiModel);
                db.SaveChanges();
                return true;
            }
            catch(Exception EX)
            {
                return false;
            }
        }
    }
}
