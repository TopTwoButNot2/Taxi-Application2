using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.Data
{
    public class RankDetails
    {
        [Key]
        public int taxiPID { get; set; }
        public string Position { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        public string RouteName { get; set; }


    }
}
