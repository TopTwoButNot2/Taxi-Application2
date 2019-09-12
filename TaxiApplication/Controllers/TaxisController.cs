using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;
using System.IO;

namespace TaxiApplication.Controllers
{
    public class TaxisController : Controller
    {
        private ApplicationDbContext db;
        private TaxiLogics taxiLogic;
        private TaxiModelLogics taximodelLogic;
        private TaxiMakeLogics taximakeLogic;
        private OwnerLogics ownerLogics;
        public TaxisController()
        {
            this.taxiLogic = new TaxiLogics();
            this.taximodelLogic = new TaxiModelLogics();
            this.taximakeLogic = new TaxiMakeLogics();
            this.ownerLogics = new OwnerLogics();
            this.db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            TempData["count"] = db.Taxis.Count();
            var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
            var taxis = db.Taxis.Where(x => x.ownerID == owner.ownerID);
            return View(taxis.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.TaxiModelID = new SelectList(taximodelLogic.GetTaxiModels(), "TaxiModelID", "TaxiModelName");
            ViewBag.MakeId = new SelectList(taximakeLogic.GetTaxiMakes(), "MakeId", "MakeType");
            ViewBag.ownerID = new SelectList(ownerLogics.GetOwners(), "ownerID", "FirstName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Taxi taxi)
        {
            ViewBag.TaxiModelID = new SelectList(taximodelLogic.GetTaxiModels(), "TaxiModelID", "TaxiModelName");
            ViewBag.MakeId = new SelectList(taximakeLogic.GetTaxiMakes(), "MakeId", "MakeType");
            ViewBag.ownerID = new SelectList(ownerLogics.GetOwners(), "ownerID", "FirstName");
            var d = db.Taxis.Where(n => n.TaxiModelID == taxi.TaxiModelID && n.MakeId == taxi.MakeId && n.TaxiNo==taxi.TaxiNo);
            if (d != null)
             {
                 TempData["AlertMessage"] = "Taxi is Already Saved on the system";
             }
             else
            {
                var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
                taxi.ownerID = owner.ownerID;

                taxiLogic.Add(taxi);
                return RedirectToAction("Index");
            }
            //var tno = db.Taxis.ToList();
            //foreach (var e in tno)
            //{
            //    if (e.TaxiNo == taxi.TaxiNo)
            //    {
            //        TempData["AlertMessage"] = "Taxi is Already Saved on the system";
            //        return View("Create");
            //    }
            //

            return View("Create");
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
        public ActionResult Delete(int? TaxiNo)
        {
            if (TaxiNo == null)
                return RedirectToAction("Bad_Request", "Error");
            if (taxiLogic.GetTaxi(TaxiNo) != null)
                return View(taxiLogic.GetTaxi(TaxiNo));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int TaxiNo)
        {
            taxiLogic.RemoveItem(taxiLogic.GetTaxi(TaxiNo));
            return RedirectToAction("Index");
        }


    }
}