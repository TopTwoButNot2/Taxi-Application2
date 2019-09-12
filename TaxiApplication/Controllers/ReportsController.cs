using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class ReportsController : Controller
    {
        private ReportService rlogic;
        public ReportsController()
        {
            this.rlogic = new ReportService();
        }
        // GET: Reports
        public ActionResult Index()
        {
            return View(rlogic.GetReports());
        }
    }
}