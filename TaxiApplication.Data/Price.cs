using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
    public class Price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceID { get; set; }

        [Required, Range(0, 25000)]
        [DisplayFormat(DataFormatString = "{0: 0.00}")]
        [DisplayName("Price")]

        public decimal pricevalue { get; set; }

        public virtual Route Route { get; set; }

        public int RouteId { get; set; }

        public virtual Season season { get; set; }
        public int SeasonID { get; set; }
        public string Seannnn { get; set; }
        [DisplayName("Date From")]
        public DateTime DateF { get; set; }
        [DisplayName("Date To")]
        public DateTime DateT { get; set; }
        public string Routeee { get; set; }
        ApplicationDbContext db=new ApplicationDbContext();

        public string SelectDescription()
        {
            var D = db.Seasons.Where(n => n.SeasonID == SeasonID).Select(n => n.Description).FirstOrDefault();
            return D;
        }
        public DateTime SelectStartDates()
        {
            var D = db.Seasons.Where(n => n.SeasonID == SeasonID).Select(n => n.StartDate).FirstOrDefault();
            return D;
        }

        public DateTime SelectEndDates()
        {
            var D = db.Seasons.Where(n => n.SeasonID == SeasonID).Select(n => n.EndDate).FirstOrDefault();
            return D;
        }

        public string SelectRouteName()
        {
            var r = db.Routes.Where(n => n.RouteId == RouteId).Select(n => n.RouteName).FirstOrDefault();
            return r;
        }

    }
}