using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace TaxiApplication.Data
{
    public class Insurance
    {
        [Key]
        //[Required(ErrorMessage ="Insurance ID Required")]
        //[Display (Name= "Insurance Calm Number")]
        public int InsuranceId { get; set; }
        public string Insurance_Type { get; set; }
        [Range(0, 10000)]
        [Display(Name = "Insurance Excess fee")]
        public double Insurance_Excess_Fee { get; set; }
        public ICollection<Taxi> taxi { get; set; }
    }
}