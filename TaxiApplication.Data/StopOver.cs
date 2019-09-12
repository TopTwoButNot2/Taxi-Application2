using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
    public class StopOver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StopID { get; set; }


        public double lat { get; set; }
        public double longitude { get; set; }

        public string stop { get; set; }

        public virtual Route Route { get; set; }
        public int RouteId { get; set; }

    }
}