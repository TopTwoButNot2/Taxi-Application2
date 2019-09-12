using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TaxiApplication.Models
{
    public class TaxiPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Position { get; set; }
        public virtual Taxi taxi { get; set; }
        public string TaxiNo { get; set; }
        public virtual Driver driver { get; set; }
        public string driverID { get; set; }
        public virtual Schedule schedule { get; set; }
        public IEnumerable<int> No { get; set; }
    }
}