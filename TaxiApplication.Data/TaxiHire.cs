using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.Data
{
    public class TaxiHire
    {
        [Key]
        public int vehicleID { get; set; }
        [Display(Name = "Taxi Make")]

        public string MakeId { get; set; }
        public virtual TaxiMake TaxiMakes { get; set; }
        [Display(Name = "Taxi Model")]

        public string TaxiModelID { get; set; }
        public virtual TaxiModel TaxiModels { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Cost Per Night")]
        public double Cost_Per_Day { get; set; }

        [Display(Name = "Kilometre Rate")]
        public double KiloRate { get; set; }
        public double BasicPrice { get; set; }
        [Display(Name = "Transmisssion Type")]
        public int transID { get; set; }
        public Transmission Transmission { get; set; }
        [Display(Name = "Fuel Type")]
        public int FuelID { get; set; }
        public Fuel Fuel { get; set; }
        public string Status { get; set; } = "Available";
        [Display(Name = "Free kilo meters")]
        public double freeKiloMeters { get; set; }
        public double CurrentMilage { get; set; }
        [Display(Name = "Insurance")]
        public int InsuranceId { get; set; }
        public int numTimesBooked { get; set; }
        public virtual Insurance Insurances { get; set; }
        public string desciption { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        [Display(Name = "Seats")]
        public int seasts { get; set; }
        public string ownerID { get; set; }
        public virtual Owner owner { get; set; }
        public ICollection<Extra> Extra { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public string getMake()
        {
            var type = (from t in db.TaxiMakes
                        where t.MakeId == MakeId
                        select t.MakeId).FirstOrDefault();
            return type;
        }
        public string getModel()
        {
            var model = (from t in db.TaxiModels
                        where t.TaxiModelID == TaxiModelID
                         select t.TaxiModelName).FirstOrDefault();
            return model;
        }
    }
}
