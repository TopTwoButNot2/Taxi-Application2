using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace TaxiApplication.Data
{
   public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { get; set; }

        [Required]

        public int Position { get; set; }

        // public int Row { get; set; }
        public string ownerID { get; set; }
        public virtual Owner owner { get; set; }
        public string RankId { get; set; }
        public virtual Rank rank { get; set; }
        public int RouteId { get; set; }
        public virtual Route route { get; set; }
        public string Ss { get; set; }
        public decimal RoutePrice { get; set; }
        public string Routeee { get; set; }

        [DataType(DataType.Time)]
        public DateTime LoadingTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime DepertureTime { get; set; }

        public int id { get; set; }
        public virtual ScheduleDate ScheduleDate { get; set; }

        public virtual ICollection<TaxiPosition> TaxiPositions { get; set; }

        ApplicationDbContext db=new ApplicationDbContext();

        public string SelectSeason()
        {
            var s = db.Seasons.Where(n => n.StartDate < DateTime.Now && n.EndDate > DateTime.Now)
                .Select(n => n.Description).FirstOrDefault();
            return s;
        }
        public decimal SelectPrice()
        {
            var s = db.Prices.Where(n => n.RouteId==RouteId && n.Seannnn==Ss).Select(n=>n.pricevalue).FirstOrDefault();
               
            return s;
        }

        public decimal Price()
        {
            var c = (from s in db.Seasons
                     join p in db.Prices on s.SeasonID equals p.SeasonID
                     select p.pricevalue);
            return Convert.ToDecimal(c);
        }

        public string SelectRoute()
        {
            var r = db.Routes.Where(n => n.RouteId == RouteId).Select(n => n.RouteName).FirstOrDefault();
            return r;
        }

        public decimal getPrice()
        {
            var s = db.Prices.Where(n => n.SelectStartDates() < DateTime.Now && n.SelectEndDates() > DateTime.Now && n.Route.RouteId==RouteId)
                .Select(n => n.pricevalue).FirstOrDefault();
            return s;
        }
         


        
    }
}
