using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;

namespace TaxiApplication.BusinessLogics
{
   public class PositionServices
    {
        private readonly ApplicationDbContext _context;
        public PositionServices()
        {
            _context = new ApplicationDbContext();
        }

        public List<Position> GetTaxiPositions()
        {
            var listOfTaxiPositions = _context.Positions
                                    .Include(n => n.schedule)
                                    .Include(n => n.TaxiDriver).ToList();


            return listOfTaxiPositions;
        }

        public bool Add(Position taxiPosition)
        {
            try
            {
                _context.Positions.Add(taxiPosition);
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

                Available av = new Available();

                av.ID = taxiPosition.ID;
                av.td = taxiPosition.td;
                av.No = taxiPosition.No;
                av.DriverName = taxiPosition.DriverName;
                av.NumSeats = taxiPosition.NumSeats;
                //av.ReservedSeats = taxiPosition.ReservedSeats;
                //av.RoutePrice = av.SelectPrice();
                _context.Availables.Add(av);
                //  db.TaxiPosition.Remove(taxiPosition);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            { return false; }
        }
    }
}

