using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.Data
{
    public class PassengerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string RouteName { get; set; }
        public string DriverName { get; set; }
        public string PhoneNumber { get; set; }
        public string Taxi_RegNo { get; set; }
        public string TaxiMake { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal RoutePrice { get; set; }
        public double TotalPrice { get; set; }
        public string DepartureDate { get; set; }
        public Nullable<int> NumberLaggagSmall { get; set; }
        public Nullable<int> NumberLaggageMed { get; set; }
        public Nullable<int> NumberLaggageLrg { get; set; }

    }
}
