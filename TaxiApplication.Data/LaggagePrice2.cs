using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace TaxiApplication.Data
{
   public class LaggagePrice2
    {
        [Key]
        public int LPId { get; set; }
        public int SeasonID { get; set; }
        public virtual Season season { get; set; }
        public double Price { get; set; }
       public string Size { get; set; }
    }
}
