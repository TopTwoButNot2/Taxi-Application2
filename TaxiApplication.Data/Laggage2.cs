using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
   public class Laggage2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
         
        public string PassengerId { get; set; }
        public virtual Passenger passenger { get; set; }

        public Nullable<int> NumberLaggagSmall { get; set; }

        public Nullable<int> NumberLaggageMed { get; set; }

        public Nullable<int> NumberLaggageLrg { get; set; }

        public double TotalPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }





        public int Count { get; set; }




        public int CalcCount()
        {
            Count = 0;
            return Count = Count + 1;
        }
        public double CalcSmallPrice()
        {
            return Convert.ToDouble(NumberLaggagSmall);
        }
        public double CalcMediumPrice()
        {
            return Convert.ToDouble(NumberLaggageMed);
        }
        public double CalLargePrice()
        {
            return Convert.ToDouble(NumberLaggageLrg);
        }

    }
}

    
   