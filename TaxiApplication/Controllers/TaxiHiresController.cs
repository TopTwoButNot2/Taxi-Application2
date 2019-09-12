using System;
 using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
 using System.IO;
using TaxiApplication.Data;
using Microsoft.AspNet.Identity;

namespace TaxiApplication.Controllers
{
    public class TaxiHiresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaxiHires
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var TaxiHires = db.TaxiHires.Include(v => v.Fuel).Include(v => v.Insurances).Include(v => v.Transmission);
            return View(TaxiHires.ToList());
        }
        
        public ActionResult BookVehicle()
        {
            var TaxiHires = db.TaxiHires.Include(v => v.Fuel).Include(v => v.Insurances).Include(v => v.Transmission);
            return View(TaxiHires.ToList());
        }
        // GET: TaxiHires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiHire vehicle = db.TaxiHires.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: TaxiHires/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
          
            ViewBag.MakeId = new SelectList(db.TaxiMakes, "MakeId", "MakeType");
            ViewBag.TaxiModelID = new SelectList(db.TaxiModels, "TaxiModelID", "TaxiModelName");

            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelName");
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "Insurance_Type");
            ViewBag.transID = new SelectList(db.Transmissions, "transID", "transName");
            return View();
        }

        // POST: TaxiHires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vehicleID,MakeId,TaxiModelID,BasicPrice,Cost_Per_Day,KiloRate,transID,FuelID,Status,freeKiloMeters,,CurrentMilage,InsuranceId,numTimesBooked,desciption,Image,ImageType,seasts")] TaxiHire vehicle, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                vehicle.ImageType = Path.GetExtension(file.FileName);
                vehicle.Image = ConvertToBytes(file);
            }
            //var Owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
            //vehicle.ownerID = Owner.Email;
            if (ModelState.IsValid)
            {
              
                db.TaxiHires.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

   
            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelName", vehicle.FuelID);
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "Insurance_Type", vehicle.InsuranceId);
            ViewBag.transID = new SelectList(db.Transmissions, "transID", "transName", vehicle.transID);
            return View(vehicle);
        }
        public JsonResult GetModelList(string vMakeID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<TaxiModel> listModel = db.TaxiModels.Where(x => x.MakeId == vMakeID).ToList();

            return Json(listModel, JsonRequestBehavior.AllowGet);
        }
        // GET: TaxiHires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiHire vehicle = db.TaxiHires.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            List<TaxiMake> listMake = db.TaxiMakes.ToList();
            ViewBag.MakeId = new SelectList(listMake, "MakeId", "MakeType");
            ViewBag.TaxiModelID = new SelectList(db.TaxiModels, "TaxiModelID", "TaxiModelName");

            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelName", vehicle.FuelID);
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "Insurance_Type", vehicle.InsuranceId);
            ViewBag.transID = new SelectList(db.Transmissions, "transID", "transName", vehicle.transID);
            return View(vehicle);
        }

        // POST: TaxiHires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vehicleID,MakeId,TaxiModelID,BasicPrice,Cost_Per_Day,KiloRate,transID,FuelID,Status,cgID,freeKiloMeters,,CurrentMilage,InsuranceId,numTimesBooked,desciption,Image,ImageType,seasts")] TaxiHire vehicle, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                vehicle.ImageType = Path.GetExtension(file.FileName);
                vehicle.Image = ConvertToBytes(file);
            }
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
 
            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelName", vehicle.FuelID);
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "Insurance_Type", vehicle.InsuranceId);
            ViewBag.transID = new SelectList(db.Transmissions, "transID", "transName", vehicle.transID);
            return View(vehicle);
        }

        // GET: TaxiHires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiHire vehicle = db.TaxiHires.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: TaxiHires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxiHire vehicle = db.TaxiHires.Find(id);
            db.TaxiHires.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // Convert file to binary
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // Display File
        public FileStreamResult RenderImage(int id)
        {
            MemoryStream ms = null;

            var item = db.TaxiHires.FirstOrDefault(x => x.vehicleID == id);
            if (item != null)
            {
                ms = new MemoryStream(item.Image);
            }
            return new FileStreamResult(ms, item.ImageType);
        }
    }
}
