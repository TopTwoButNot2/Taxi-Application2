using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class ReportsVM
    {
     
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }

}