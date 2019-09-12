using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class ReservedTaxi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Position { get; set; }
       // public virtual Taxi taxi { get; set; }
        public string TaxiNo { get; set; }
        public virtual Driver driver { get; set; }
        public string driverID { get; set; }
        public virtual Schedule schedule { get; set; }
        public Nullable<int> No { get; set; }
        public int Count { get; set; }
        
    }
}