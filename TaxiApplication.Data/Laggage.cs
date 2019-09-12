using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace TaxiApplication.Data
{
    public class Laggage
    {
        [Key]
        public int LaggageID { get; set; }           
        public int NumberLaggage { get; set; }
        public byte[] picture { get; set; }
        public string Size { get; set; }
        public string status { get; set; }

        public double TotalPrice { get; set; }
        public string PassengerName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd - MM - yyyy}", ApplyFormatInEditMode = true)]

        public string PassengerId { get; set; }
        [ForeignKey("PassengerId")]
        public Passenger passengers { get; set; }

        //public int ID { get; set; }
        //[ForeignKey("ID")]

        //public Available GetAvailable { get; set; }


        public virtual Schedule schedule { get; set; }
        public Nullable<int> No { get; set; }

        
        ApplicationDbContext db = new ApplicationDbContext();
        public List<LaggageSetting> setting()
        {
            return db.LaggageSettings.ToList();
        }
        public List<LaggagePrices> pricess()
        {
            return db.LaggagePrice.ToList();
        }
        public string getSize()
        {
            string sizz = "";
            foreach(var item in setting())
            {
                if (NumberLaggage > item.Test && NumberLaggage<= item.NumOfBags)
                {
                    sizz = item.Size;
                }

            }
            return sizz;
        }

        public decimal getPrice()
        {
            decimal price = 0;
            foreach (var item in pricess())
            {
                if (getSize() == item.ls.Size)
                {
                    price = item.price;
                }

            }
            return price;
        }

       
        //public decimal price(int p)
        //{
        //    Price mk = db.Prices.ToList().Find(x => x.PriceID == p);
        //    return mk.pricevalue;
        //}
        //public string stopover(int s)
        //{
        //    StopOver mk = db.StopOvers.ToList().Find(x => x.StopID == s);
        //    return mk.stop;
        //}
        //public string taxii(string t)
        //{
        //    Taxi mk = db.Taxis.ToList().Find(x => x.TaxiNo == t);
        //    return mk.RegNo;
        //}

    }
}