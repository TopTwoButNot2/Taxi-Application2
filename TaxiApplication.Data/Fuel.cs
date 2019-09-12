using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace TaxiApplication.Data
{
    public class Fuel
    {
        [Key]
        public int FuelID { get; set; }
        public string FuelName { get; set; }

        public ICollection<Taxi> taxi { get; set; }
    }
}