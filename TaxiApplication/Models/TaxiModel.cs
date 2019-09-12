using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Models
{
    public class TaxiModel
    {
        [Key]
        public string TaxiModelID { get; set; }
        public string TaxiModelName { get; set; }
        public int Seats { get; set; }

        public ICollection<Taxi> Taxis { get; set; }
    }
}