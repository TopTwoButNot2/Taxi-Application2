using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Data
{
    public class LaggageSetting
    {
        [Key]
        public int laggID { get; set; }
        [Display(Name = "End Value")]

        public int NumOfBags { get; set; }
        public string Size { get; set; }
        [Display(Name ="Start Value")]
        public int Test { get; set; }

    }
}