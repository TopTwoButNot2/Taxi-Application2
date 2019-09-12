using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;
using TaxiApplication.Models;

namespace TaxiApplication.Controllers
{
    public class ErrorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Bad_Request()
        {
            return View();
        }
        public ActionResult Not_Found()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        public JsonResult Update(Route route)
        {
            var status = false;
            if (route != null)
            {
                var obj = db.Routes.Where(m => m.RouteId == route.RouteId).FirstOrDefault();
                obj.RouteDistance = route.RouteDistance;
                obj.RouteDuration = route.RouteDuration;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult DemoS()
        {
            //ApplicationDbContext db = new ApplicationDbContext();

            //db.Configuration.ProxyCreationEnabled = false;
            //var workSchedules = db.Destinations.ToList();
            ////Tempory holder for resrouces
            //List<ClassRoom> resList = new List<ClassRoom>();


            //foreach (var wrks in workSchedules)
            //{
            //    //Finds Employee Linked to Event


            //    //Saves All information about event to temporary result model
            //    ClassRoom res = new ClassRoom();

            //    res.roomNumber = wrks.roomNumber;

            //    res.classRoomId = wrks.classRoomId;
            //    res.GradeName = wrks.GradeName;
            //    //TimeSpan? timeDif = res.scheduleEndDate - res.scheduleStartDate;
            //    //res.Description = tempEmp.Occupation + ", works " + timeDif.Value.TotalHours + " hours today.";

            //    resList.Add(res);
            //}

            return new JsonResult { Data = db.Routes.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}