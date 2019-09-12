using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class TaxiModelsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private TaxiModelLogics tmLogic;
        public TaxiModelsController()
        {
            this.tmLogic = new TaxiModelLogics();
        }
        // GET: TaxiModels
        public ActionResult Index()
        {
            return View(tmLogic.GetTaxiModels());
        }
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(db.TaxiMakes, "MakeId", "MakeType");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxiModel taxiMode)
        {
            if(ModelState.IsValid)
            {
                //var name = taxiMode.TaxiModelName.Split(';');
                //foreach (var n in name)
                //{
                    //taxiMode.TaxiModelName = n;
                    tmLogic.Add(taxiMode);
                //}
                
                return RedirectToAction("Index");
            }
            ViewBag.MakeId = new SelectList(db.TaxiMakes, "MakeId", "MakeType", taxiMode.MakeId);

            return View();
        }
    }
}