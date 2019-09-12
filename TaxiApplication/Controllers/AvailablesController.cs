using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
    public class AvailablesController : Controller
    {
        private AvailableLogic aLogic;
        public AvailablesController()
        {
            this.aLogic = new AvailableLogic();
        }
        // GET: Availables
        public ActionResult Index()
        {
            return View(aLogic.GetAvailables());
        }
        public ActionResult Check()
        {
            return View(aLogic.GetAvailables());
        }

        public ActionResult Reserve()
        {
            return RedirectToAction("Create", "Reservations");
        }
        //public ActionResult Reserve(int? ID)
        //{
        //    if (ID == null)
        //        return RedirectToAction("Bad_Request", "Error");
        //    if (aLogic.GetAvailable(ID) != null)
        //        return View(aLogic.GetAvailable(ID));
        //    else
        //        return RedirectToAction("Not_Found", "Error");
        //}
        //[HttpPost, ActionName("Reserve")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Reserve(int ID)
        //{
        //    aLogic.Reserve(aLogic.GetAvailable(ID));
        //    return RedirectToAction("Create","Laggages", new { id = ID });
        //}
        public ActionResult CheckOut(int? ID)
        {
            if (ID == null)
                return RedirectToAction("Bad_Request", "Error");
            if (aLogic.GetAvailable(ID) != null)
                return View(aLogic.GetAvailable(ID));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(int ID)
        {
            aLogic.CheckOut(aLogic.GetAvailable(ID));
            return RedirectToAction("Index", "Reports");
        }
        public JsonResult GetMapPoints(string taxiId)
        {
            return Json(aLogic.GetMapDetails(taxiId), JsonRequestBehavior.AllowGet);
        }
    }
}