using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Models
{
    public class TaxiMake
    {
        [Key]
        public string MakeId { get; set; }
        [Required]

        public string MakeType { get; set; }

        public ICollection<Taxi> Taxis { get; set; }
    }
}