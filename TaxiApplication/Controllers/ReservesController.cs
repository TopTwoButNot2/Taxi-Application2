using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class ReservesController : Controller
    {
        private ReservedServices rlogic;
        public ReservesController()
        {
            this.rlogic = new ReservedServices();
        }
        // GET: Reserves
        public ActionResult Index()
        {
            return View(rlogic.GetReserved());
        }
    }
}