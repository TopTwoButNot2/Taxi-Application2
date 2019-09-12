using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
    public class LuggagePrice
    {
        [Key]
        public  int PriceID { get; set; }
        public string Luggage { get; set; }
        public double  price { get; set; }

    }
}
