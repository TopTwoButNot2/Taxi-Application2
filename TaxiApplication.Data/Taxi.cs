using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TaxiApplication.Data
{
    public class Taxi
    {
        [Key]
        public string TaxiNo { get; set; }

        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Status Name must be atleast 2 characters long", MinimumLength = 2)]

        public virtual Owner owner { get; set; }
        public string ownerID { get; set; }
        public byte[] Image { get; set; }
        public virtual TaxiMake TaxiMake { get; set; }
        public string MakeId { get; set; }
        public string TaxiMaketype { get; set; }
        public string TaxiModeltype { get; set; }

        public virtual TaxiModel TaxiModel { get; set; }
        public string TaxiModelID { get; set; }
        public int NumSeats { get; set; }

       
    }
}