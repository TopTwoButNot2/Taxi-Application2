using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class WalkinPassengersController : Controller
    {
        private WalkinPassengerLogic walkLogic;
        public WalkinPassengersController()
        {
            this.walkLogic = new WalkinPassengerLogic();
        }


        // GET: WalkinPassengers
        public ActionResult Index()
        {
            return View(walkLogic.GetWalkingPassengers());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WalkingPassenger walkingPassenger)
        {
            if (ModelState.IsValid)
            {
                walkLogic.AddWalkin(walkingPassenger);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}