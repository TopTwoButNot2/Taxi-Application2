using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Data
{
    public class LaggagePrices
    {
        [Key]
        public int LagID { get; set; }
        public decimal price { get; set; }
        public int laggID { get; set; }
        public virtual LaggageSetting ls { get; set; }
    }
}