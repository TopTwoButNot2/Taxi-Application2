using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
    public class ReservedPassengers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResPasID { get; set; }
        public string RouteName { get; set; }
        public decimal RoutePrice { get; set; }
        public string TaxiNo { get; set; }
        public string OwnerId { get; set; }
        public string DtriverName { get; set; }
        public string DriverId { get; set; }
        public int NumSeats { get; set; }

        public int ReservedSeats { get; set; }
        public int AvailableSeats { get; set; }
        public int ID { get; set; }
        public virtual TaxiPosition TaxiPosition { get; set; }

        public string PassengerId { get; set; }
        public virtual Passenger Passenger { get; set; }




    }
}





















