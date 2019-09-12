using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace TaxiApplication.Data
{
   public  class ReservedPassengersViewModel
    {
        [Key]
        public int VMKY { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        public string TaxiNo { get; set; }
        public string RouteName { get; set; }
        public Nullable<int> NumberLaggagSmall { get; set; }

        public Nullable<int> NumberLaggageMed { get; set; }

        public Nullable<int> NumberLaggageLrg { get; set; }

        public double TotalPrice { get; set; }
    }
}
