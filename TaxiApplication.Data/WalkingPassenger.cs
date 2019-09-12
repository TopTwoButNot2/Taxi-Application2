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
   public class WalkingPassenger
    {
        [Key]
        
        public string PassengerId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Increment { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string TaxiNo { get; set; }

        public string RouteName { get; set; }

        public string StopOver { get; set; }

        public string DriverName { get; set; }
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
            return Convert.ToDouble( NumberLaggagSmall);
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
