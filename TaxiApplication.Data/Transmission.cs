using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Data
{
    public class Transmission
    {
        [Key]
        public int transID { get; set; }
        [Display(Name = "Transmission")]
        public string transName { get; set; }
        public ICollection<Taxi> taxi { get; set; }
    }
}