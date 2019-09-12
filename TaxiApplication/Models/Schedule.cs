using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Models
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
        [Required]

        public int Position { get; set; }

        // public int Row { get; set; }
        public string ownerID { get; set; }
        public virtual Owner owner { get; set; }
        public string RankId { get; set; }
        public virtual Rank rank { get; set; }
        public int RouteId { get; set; }
        public virtual Route route { get; set; }
    }
}