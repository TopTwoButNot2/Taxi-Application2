using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaxiApplication.Data
{
   public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int td { get; set; }
        public virtual TaxiDriver TaxiDriver { get; set; }

        public virtual Schedule schedule { get; set; }
        public int No { get; set; }

        public string DriverName { get; set; }
        public string Week { get; set; }
        public int NumSeats { get; set; }
        public int ReservedSeats { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastUpadated { get; set; }

    }
}
