using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
   public class ReservedViewModel
    {
        [Key]
        public string PassengerId { get; set; }

        public string RouteName { get; set; }
        public decimal RoutePrice { get; set; }
        public string DriverName { get; set; }
        public string TaxiNo { get; set; }
        public int OwnerTel { get; set; }
        public string DriverTel { get; set; }
        public string TaxiMake { get; set; }
        public string TaxiModel { get; set; }
        public byte[] Image { get; set; }
        public string LoadingTime { get; set; }
        public string DepertureTime { get; set; }
        public string PassengerName { get; set; }
        public string PassengerTel { get; set; }


        public Nullable<int> NumberLaggagSmall { get; set; }

        public Nullable<int> NumberLaggageMed { get; set; }

        public Nullable<int> NumberLaggageLrg { get; set; }

        public double LaggagePrice { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}
