using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class TaxiMake
    {
        [Key]
        public string MakeId { get; set; }
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "MakeType Name must be at least 2 characters long", MinimumLength = 2)]
        public string MakeType { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        //  public ICollection<Taxi> Taxis { get; set; }
    }
}

