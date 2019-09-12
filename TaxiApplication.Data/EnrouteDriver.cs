using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.Data
{
    public class EnrouteDriver
    {
        public string driverId { get; set; }
        public Rank rank { get; set; }
        public Taxi taxi { get; set; }


    }
}
