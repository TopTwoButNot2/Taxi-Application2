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
   public class WalkinDetails
    {
         [Key]
        public string PassengerId { get; set; }

        public string FirstName { get; set; }
         
        public string TaxiNo { get; set; }

        public string RouteName { get; set; }

        public string StopOver { get; set; }
        public double TotalPrice { get; set; }

        public string DriverName { get; set; }
       
        public DateTime DepartureDate { get; set; }

        public string LName { get; set; }
       

        public string PhoneNumber { get; set; }
       

        public string Address { get; set; }
    }
}
