using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Data
{
    public class Extra
    {
        [Key]
        public int extrasID { get; set; }

        public double ExtraPrice { get; set; }
        public string ExtraType { get; set; }

    }

    public class ListExtra
    {
        public List<Extra> Extra { get; set; }
    }
}