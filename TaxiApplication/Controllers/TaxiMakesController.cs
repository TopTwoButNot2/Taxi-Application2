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
    public class TaxiMakesController : Controller
    {
        private ApplicationDbContext db;
        private TaxiMakeLogics tmLogic;
        public TaxiMakesController()
        {
            this.db = new ApplicationDbContext();
            this.tmLogic = new TaxiMakeLogics();
        }
        // GET: TaxiMakes
        public ActionResult Index()
        {
            return View(tmLogic.GetTaxiMakes());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (tmLogic.GetTaxiMake(id) != null)
                return View(tmLogic.GetTaxiMake(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxiMake taxiMake, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                taxiMake.ImageType = Path.GetExtension(file.FileName);
                taxiMake.Image = ConvertToBytes(file);
            }
            var taxiMakes = db.TaxiMakes.Where(x => x.MakeType == taxiMake.MakeType);
            if (taxiMakes.Count() != 0)
            {
                TempData["AlertMessage"] = "The Taxi make already exists";
                ModelState.AddModelError("", "");
            }

            if (ModelState.IsValid)
            {
                tmLogic.AddTaxiMake(taxiMake);
                return RedirectToAction("Index");
            }

            return View();
        }
        // Convert file to binary
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

        public ActionResult Edit(int? id)
        {

            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (tmLogic.GetTaxiMake(id) != null)
                return View(tmLogic.GetTaxiMake(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaxiMake taxiMake)
        {
            if (ModelState.IsValid)
            {
                tmLogic.UpdateTaxiMake(taxiMake);
                return RedirectToAction("Index");
            }
            return View(taxiMake);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (tmLogic.GetTaxiMake(id) != null)
                return View(tmLogic.GetTaxiMake(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tmLogic.RemoveTaxiMake(tmLogic.GetTaxiMake(id));
            return RedirectToAction("Index");
        }
    }
}