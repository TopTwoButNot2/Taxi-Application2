using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.Models;

namespace TaxiApplication.Controllers
{
    public class RouteViewModelsController : Controller
    {
        // GET: RouteViewModels
        public ActionResult Index()
        {
            return View();
        }
    
        //public ActionResult FormalSchedule(int RouteId)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    List<RouteViewModel> vm = new List<RouteViewModel>();
        //    var list = (from op in db.Routes
        //                join rp in db.Ranks on op.RankId equals rp.RankId
        //                select new
        //                {
        //                    op.RouteId,
        //                    rp.RankId,
        //                    rp.RankName,
        //                    op.RouteName,
        //                    op.RouteDistance,
        //                    op.isAvailable
        //                }).ToList();
        //    RouteViewModel v = new RouteViewModel();

        //    foreach (var x in list)
        //    {

        //        v.RankId = x.RankId;
        //        v.RouteId = x.RouteId;
        //        v.RankName = x.RankName;
        //        v.RouteName = x.RouteName;
        //        v.RouteDistance = x.RouteDistance;
        //        v.status = x.isAvailable.ToString();
        //        vm.Add(v);
        //    }
            
        //    if (v.RouteId == RouteId)
        //    {
        //        return View(vm);

        //    }
        //    else
        //    {
        //        ModelState.AddModelError("","Dfdggfszdbgf");
        //        return View();

        //    }

        //}

    }
}