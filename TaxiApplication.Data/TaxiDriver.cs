using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class TaxiDriver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int td { get; set; }
        public string driverID { get; set; }
        public virtual Driver driver { get; set; }
        public string TaxiNo { get; set; }
        public virtual Taxi taxi { get; set; }
        public string ownerID { get; set; }
        public virtual Owner owner { get; set; }
        public int NumSeats { get; set; }
        public byte[] Image { get; set; }

        public string TaxiMake { get; set; }
        public string TaxiModel { get; set; }
        public string DriverTel { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public int SelectSeats()
        {
            var ss = db.Taxis.Where(n => n.TaxiNo == TaxiNo).Select(n => n.NumSeats).FirstOrDefault();
            return ss;
            
        }
        

    }
}
