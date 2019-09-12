using System.Linq;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;
using System.IO;
using System.Web;

namespace TaxiApplication.Controllers
{
    public class TaxiDriversController : Controller
    {
        private ApplicationDbContext db;
        private TaxiDriverLogics txDriverLogic;
        private DriverLogics dLogic;
        private TaxiLogics tlogic;

        public TaxiDriversController()
        {
            this.txDriverLogic = new TaxiDriverLogics();
            this.tlogic = new TaxiLogics();
            this.dLogic = new DriverLogics();
            this.db = new ApplicationDbContext();
        }
        // GET: TaxiDrivers
        public ActionResult Index()
        {
            var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
            var taxis = db.TaxiDrivers.Where(x => x.ownerID == owner.ownerID);
            return View(taxis.ToList());
        }
        public ActionResult DriverIndex()
        {
            var Driver = db.Drivers.ToList().Find(x => x.Email == User.Identity.Name);
            var taxis = db.TaxiDrivers.Where(x => x.driverID == Driver.driverID);
            return View(taxis.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.driverID = new SelectList(dLogic.GetDrivers(User.Identity.Name), "driverID", "FirstName");
            ViewBag.TaxiNo = new SelectList(tlogic.GetTaxis(User.Identity.Name), "TaxiNo", "TaxiNo");
            //ViewBag.ownerID = new SelectList(tlogic.GetTaxis(), "ownerID", "FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxiDriver taxiDriver)
        {
            ViewBag.driverID = new SelectList(dLogic.GetDrivers(User.Identity.Name), "driverID", "FirstName");
            ViewBag.TaxiNo = new SelectList(tlogic.GetTaxis(User.Identity.Name), "TaxiNo", "TaxiNo");

            var driverTel = db.Drivers.Where(x => x.driverID == taxiDriver.driverID).Select(x => x.PhoneNumber).FirstOrDefault();
            
            var taximodeltype = db.Taxis.Where(x => x.TaxiNo == taxiDriver.TaxiNo).Select(x => x.TaxiModeltype).FirstOrDefault();
            
            var taxiMaketype = db.Taxis.Where(x => x.TaxiNo == taxiDriver.TaxiNo).Select(x => x.TaxiMaketype).FirstOrDefault();
            var image = db.Taxis.Where(x => x.TaxiNo == taxiDriver.TaxiNo).Select(x => x.Image).FirstOrDefault();


            var d = db.TaxiDrivers.Where(n =>  n.TaxiNo == taxiDriver.TaxiNo);

            var dr = db.TaxiDrivers.Where(x => x.driverID == taxiDriver.driverID);

             if(ModelState.IsValid)
             {
                var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);

                taxiDriver.ownerID = owner.ownerID;


                taxiDriver.Image = image;
                taxiDriver.DriverTel = driverTel;
                taxiDriver.TaxiMake = taxiMaketype;
                taxiDriver.TaxiModel = taximodeltype;

                txDriverLogic.Add(taxiDriver);
                return RedirectToAction("Index");
            }
            // else if (dr !=null)
            // {
            //     TempData["AlertMessage"] = "Driver is Already assigned to a taxi";
            //    return View();
            ////}
            //else 
            //{

            //    TempData["AlertMessage"] = "Taxi is Already assigned to a driver";
            //    return View();
                
            //}
            return View("Index");
        }
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        //Display File
        public FileStreamResult RenderImage(string id)
        {
            MemoryStream ms = null;

            var item = db.TaxiMakes.FirstOrDefault(x => x.MakeId == id);
            if (item != null)
            {
                ms = new MemoryStream(item.Image);
            }
            return new FileStreamResult(ms, item.ImageType);
        }
        public ActionResult Delete(int? td)
        {
            if (td == null)
                return RedirectToAction("NotFound", "");
            if (txDriverLogic.GetTaxiDriver(td) != null)
                return View(txDriverLogic.GetTaxiDriver(td));
            else
                return RedirectToAction("Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int td)
        {
            txDriverLogic.Delete(txDriverLogic.GetTaxiDriver(td));
            return RedirectToAction("Index");
        }


    }
}