using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;

namespace TaxiApplication.BusinessLogics
{
    public class TaxiPositionLogics
    {
        private readonly ApplicationDbContext _context;
        public TaxiPositionLogics()
        {
            _context = new ApplicationDbContext();
        }


        public List<TaxiPosition> GetOwnDetails(string email)
        {
            List<TaxiPosition> list = new List<TaxiPosition>();
            var owner = _context.Owners.ToList().Find(x => x.Email == email);
            foreach (var e in _context.TaxiPosition)
            {
                if (e.OwnerID == owner.ownerID)
                {
                    list.Add(e);
                }

            }

            return list;
        }

        public EnrouteDriver GetMapDetails(string taxiId)
        {
            var enroute = new EnrouteDriver()
            {
                rank = new Rank(),
                taxi = new Taxi()
            };
            try
            {
                var taxiDriver = _context.TaxiDrivers.FirstOrDefault(x => x.TaxiNo == taxiId);
                if (taxiDriver != null)
                {
                    enroute.taxi = taxiDriver.taxi;
                    enroute.driverId = taxiDriver.driverID;
                    enroute.rank = taxiDriver.owner.rank;
                }
            }
            catch { }

            return enroute;
        }
        public List<TaxiPosition> GetTaxiPositions(string routename)
        {
            //var listOfTaxiPositions = _context.TaxiPosition
            //                        .Include(n => n.schedule)
            //                        .Include(n => n.TaxiDriver).ToList();

            var listOfTaxiPositions = _context.TaxiPosition
                                   .Include(n => n.schedule)
                                   .Include(n => n.TaxiDriver).Where(n=>n.RouteName == routename).ToList();

            return listOfTaxiPositions;
        }
        public List<TaxiPosition> ListOfTaxisForPassenger(string search)
        {
           
            var listOfTaxiPositions = _context.TaxiPosition
                                   .Include(n => n.schedule)
                                   .Include(n => n.TaxiDriver).Where(x=>x.RouteName==search || search==null).ToList();

            return listOfTaxiPositions;
        }

        public List<TaxiPosition> GetTaxipositionList()
        {
        

            var listOfTaxiPositions = _context.TaxiPosition
                                   .Include(n => n.schedule)
                                   .Include(n => n.TaxiDriver).ToList();

            return listOfTaxiPositions;
        }


        public bool Add(TaxiPosition taxiPosition)
        {
            try
            {
               
                taxiPosition.RoutePrice = taxiPosition.SelecPrice();
                _context.TaxiPosition.Add(taxiPosition);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool RemoveItem(TaxiPosition taxiPosition)
        {
            try
            {
                _context.TaxiPosition.Remove(taxiPosition);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public TaxiPosition GetTaxiPosition(int? ID)
        {
            return _context.TaxiPosition.Find(ID);
        }

       
        
        public bool Avail(TaxiPosition taxiPosition)
        {
           
            try
            {
       

        ReservedList reee = new ReservedList();
                 
                reee.RouteName = taxiPosition.RouteName;
                reee.RoutePrice = taxiPosition.RoutePrice;
                reee.DriverName = taxiPosition.DriverName;
                reee.NumSeats = taxiPosition.NumSeats;
                reee.TaxiNo = taxiPosition.TaxiNo;
                reee.LoadingTime = taxiPosition.LoadingTime;
                reee.DepertureTime = taxiPosition.DepertureTime;
               

                _context.reservedList.Add(reee);
               _context.SaveChanges();

                return true;
            } 
            catch (Exception ex)
            { return false; }
        }
        public bool CheckOut(TaxiPosition avail)
        {

            try
            {
                avail.Status = "Checked Out";
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
