using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.Data
{
    public class GridPagination
    {
        public int CurrentPage { get; set; }
        public double TotalPage { get; set; } //Buttons  
        public int TotalData { get; set; } // Total count of the filtered data  
        public List<TaxiPosition> Data { get; set; }
        public int TakeCount { get; set; } = 10; // By default i am using 10 data per page  
        public TaxiPosition filters { get; set; } = new TaxiPosition(); // Search keys and value  
    }
}
