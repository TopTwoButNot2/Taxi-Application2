using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace TaxiApplication.Data
{
    public class TaxiPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int td { get; set; }
        public virtual TaxiDriver TaxiDriver { get; set; }
        public string OwnerID { get; set; }
        public string RouteName { get; set; }
        public decimal RoutePrice { get; set; }

        public int OwnerTel { get; set; }
        public string DriverTel { get; set; }
        public string TaxiMake { get; set; }
        public string Status { get; set; }
        public string TaxiModel { get; set; }
        public byte[] Image { get; set; }


        public virtual Schedule schedule { get; set; }
        public int No { get; set; }
        public string DriverName { get; set; }
        public string DriverId { get; set; }
        public int NumSeats { get; set; }
        public int Count { get; set; }
        public int ReservedCount { get; set; }
        //public int ReservedSeats { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsAvailable { get; set; }
        [DataType(DataType.Date)]

        //public string LastUpadated { get; set; }

        public string TaxiNo { get; set; }
        [DataType(DataType.Date)]

        public string LoadingTime { get; set; }
        public string DepertureTime { get; set; }


       


        ApplicationDbContext db = new ApplicationDbContext();


        public decimal SelecPrice()
        {
            var p = (from t in db.TaxiPosition
                     where t.No == No
                     select t.RoutePrice).FirstOrDefault();
            return p;
        }
        

        

    }
}
