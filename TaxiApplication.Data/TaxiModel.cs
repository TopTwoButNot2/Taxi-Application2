using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TaxiApplication.Data
{
   public class TaxiModel
    {
        [Key]
        
        public string TaxiModelID { get; set; }

        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "TaxiModel Name must be atleast 2 characters long", MinimumLength = 2)]
        public string TaxiModelName { get; set; }

        public string MakeId { get; set; }
        public TaxiMake taxiMake { get; set; }

        // public ICollection<Taxi> Taxis { get; set; }
    }
}
