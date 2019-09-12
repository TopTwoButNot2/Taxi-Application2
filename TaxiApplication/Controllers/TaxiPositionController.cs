using System;
using System.Threading.Tasks;
using System.Web.Http;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
   
    public class TaxiPositionController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public TaxiPositionController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> AvailTaxi(int id)
        {
            //find a taxi
            Available av = new Available();
            var taxiPosition = await _context.TaxiPosition.FindAsync(id);

            if (taxiPosition == null)
                return NotFound();
            taxiPosition.IsAvailable = true;
            //taxiPosition.LastUpadated = System.DateTime.Now.ToString();

            av.ID = taxiPosition.ID;
            av.td = taxiPosition.td;
            av.No = taxiPosition.No;
            av.DriverName = taxiPosition.DriverName;
            av.NumSeats = taxiPosition.NumSeats;
            //av.ReservedSeats = taxiPosition.ReservedSeats;
            av.LastUpadated = DateTime.Now.Date.ToString();
            av.Status = "On Stand";
            _context.Availables.Add(av);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
