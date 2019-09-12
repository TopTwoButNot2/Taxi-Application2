using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;


namespace TaxiApplication.Controllers
{
    public class ReservedTaxisController : Controller
    {
        private ReservedTaxiService reService;
        public ReservedTaxisController()
        {
            this.reService = new ReservedTaxiService();
        }
        // GET: ReservedTaxis
        public ActionResult Index()
        {
            return View(reService.GetReservedTaxisList());
        }
        public ActionResult Demo()
        {
            return View(reService.GetReservedTaxisList());
        }
        public ActionResult GetYourLocation()
        {
            return View();
        }
        public ActionResult ShowDistance()
        {
            return View();
        }
        public ActionResult Simulation()
        {
            return View();
        }
    }
}