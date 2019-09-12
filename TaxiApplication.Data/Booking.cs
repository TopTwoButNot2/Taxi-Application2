using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Data
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string userId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd - MM - yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pick up Date")]
        public DateTime PickUpDate { get; set; }
        [Required]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]

        public DateTime ReturnDate { get; set; }
        public int ReturnId { get; set; }
        [Display(Name = "Over Night Cost")]
        public double Booking_Cost { get; set; }
        public string status { get; set; } = "Pending";
        public int vehicleID { get; set; }
        public virtual TaxiHire taxiH { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public double MilageIn { get; set; }
        public double MilageOut { get; set; }
        public double ExtraPrice { get; set; }
        public double numOfDays { get; set; }
        public double kiloCost { get; set; }
        public double TotalCost { get; set; }
        public double FinalTotalCost { get; set; }
        [Display(Name = "Basic Cost")]
        public double DayPrice { get; set; }
        [Display(Name = "Free KiloMeteres")]
        public double freeKilos { get; set; }
        [Display(Name = "Deposite")]
        public double deposite { get; set; }
        [StringLength(maximumLength: 35, ErrorMessage = "Route Name must be at least 2 characters long", MinimumLength = 2)]
        public string RouteName { get; set; }
        public Nullable<double> RouteDistance { get; set; }
        public int seats { get; set; }
        public double UsedKM { get; set; }
       

        public Booking()
        {
            MilageOut = getCurrentMilage();
            //Extra = new SelectList(db.Extras, "extrasID", "extrasName");
        }

        ApplicationDbContext db = new ApplicationDbContext();

        //Get number of seats
        public int getNumOfSeats()
        {
            var inFee = (from c in db.TaxiHires
                         where c.vehicleID == vehicleID
                         select c.seasts).FirstOrDefault();
            return inFee;
        }
        public double GetCostperday()
        {
            var don = (from c in db.TaxiHires
                       where c.vehicleID == vehicleID
                       select c.Cost_Per_Day).FirstOrDefault();
            return don;
        }
        //Basic Price
        public double calcBasicCharge()
        {
            return seats * GetCostperday();
        }
        //KM Cost
        public double CalcKMCost()
        {
            return calcBasicCharge() * (GetKiloRate()/100);
        }
        public double getInsuranceFee()
        {
            var inFee = (from c in db.Insurances
                         where c.InsuranceId == taxiH.InsuranceId
                         select c.Insurance_Excess_Fee).FirstOrDefault();
            return inFee;
        }

        public double calcDeposite()
        {
            return calcBasicCharge() + getInsuranceFee();
        }
        //Free Kilometers
        public double GetFreeKM()
        {
            var freeKilos = (from c in db.TaxiHires
                             where c.vehicleID == vehicleID
                             select c.freeKiloMeters).FirstOrDefault();
            return freeKilos;
        }
        //Kilometer Rate
        public double GetKiloRate()
        {
            var kilo = (from c in db.TaxiHires
                        where c.vehicleID == vehicleID
                        select c.KiloRate).FirstOrDefault();
            return kilo;
        }
        //KM Cost
        public double calcKMCost()
        {
            double d = 0;
            if(RouteDistance > GetFreeKM())
            {
                d= (Convert.ToDouble(RouteDistance) - GetFreeKM()) * GetKiloRate();
            }
            else
            {
                d = 0;
            }
            return d;
        }
        public double calcFinalCost()
        {
            return calcBasicCharge() + CalcKMCost();
        }
        //GetBasicPrice
        public double GetBasicCost()
        {
            var don = (from c in db.TaxiHires
                       where c.vehicleID == vehicleID
                       select c.BasicPrice).FirstOrDefault();
            return don;
        }
      public double calcDeposit()
        {
            return GetBasicCost() * (Convert.ToDouble(RouteDistance));
        }
        public double caclFreeKM()
        {
            return (GetKiloRate()/100) * (Convert.ToDouble(RouteDistance));
        }
        public double caclOverNightFe()
        {
            return numOfDays * GetCostperday();
        }

        //public string getFirstName()
        //{
        //    ApplicationUser u = db.Users.ToList().Find(x => x.Id == userId);
        //    return u.FirstName;

        //}
        //public string getLastName()
        //{
        //    ApplicationUser u = db.Users.ToList().Find(x => x.Id == userId);
        //    return u.LastName;
        //}
        //public string getIDNumber()
        //{
        //    ApplicationUser u = db.Users.ToList().Find(x => x.Id == userId);
        //    return u.IdNumber;
        //}
        public string getEmail()
        {
            ApplicationUser u = db.Users.ToList().Find(x => x.Id == userId);
            return u.Email;
        }
        public double getCurrentMilage()
        {
            var crnt = (from c in db.TaxiHires
                        where c.vehicleID == vehicleID
                        select c.CurrentMilage).FirstOrDefault();
            return crnt;
        }

        public double calcFinalPrice()
        {
            return calcUsedKM() * GetKiloRate();
        }

        public double calcUsedKM()
        {
            return MilageIn - getCurrentMilage();
        }
        public double calcKM()
        {
            return (calcUsedKM() - Convert.ToDouble(RouteDistance) - kiloCost);
        }

        public double calcKMCOst()
        {
            return (calcKM() * calcBasicCharge())- deposite;
        }

        public double calcFreeKM()
        {
            return GetFreeKM() * numOfDays;
        }
        public double calcExtraKMCost()
        {
            if (calcUsedKM() > calcFreeKM())
            {
                return (calcUsedKM() - calcFreeKM()) * GetKiloRate();
            }
            else
            {
                return 0;
            }

        }
        public double calcKMRate()
        {
            var kiloRat = (from c in db.TaxiHires
                           where c.vehicleID == vehicleID
                           select c.KiloRate).FirstOrDefault();
            double kr = Convert.ToDouble(kiloRat) * calcExtraKMCost();
            if (calcUsedKM() > calcFreeKM())
            {
                return kr;
            }
            else
            {
                return kr = 0;
            }
        }
        //public double calcGrandTotal()
        //{
        //    return calcBasicCharge() + calcKMRate();
        //}
    }
}

