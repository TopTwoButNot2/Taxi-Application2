using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;
using PagedList;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;
using System.Data.Entity;
using System.IO;
using static System.Net.WebRequestMethods;

namespace TaxiApplication.Controllers
{
    public class RoutesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Routes
        private RouteServices rs;
        Rank rk;

        public RoutesController()
        {
            this.rs = new RouteServices();
            this.rk = new Rank();
        }
        public ActionResult ManagerIndex()
        {
             var manager = db.RankManagers.ToList().Find(x => x.Email == User.Identity.Name);

            return View(db.Routes.Include(xx=>xx.Rank).Include(xx=>xx.rankmanager).Where(x=>x.rankmanagerID==manager.rankmanagerID).ToList());
        }

        [HttpGet]
        public ActionResult RouteDetails(string routeName)
        {
            var route = db.Routes.ToList().Find(x => x.RouteName == routeName);
            Session["rid"] = route.RouteId;

            List<RouteViewModel> gd = new List<RouteViewModel>();
            var list = (from u in db.Ranks
                        join r in db.Routes on u.RankId equals r.RankId
                        join rm in db.Prices on r.RouteId equals rm.RouteId
                        select new
                        {
                            u.RankName,
                            r.RouteName,                           
                            rm.pricevalue
                        }).ToList();
            foreach (var it in list)
            {
                RouteViewModel gdd = new RouteViewModel();
                gdd.RankName = it.RankName;
                gdd.RouteName = it.RouteName;
                gdd.Pricevalue = it.pricevalue;
               
                gd.Add(gdd);
            }

            
            return View(gd);
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Search(string searchString, string FilterBy, int? Minimum, int? Maximum, string RouteName)
        {


            var name = from s in db.Routes
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                name = name.Where(s => s.RouteName.ToUpper().Contains(searchString.ToUpper()));
            }
            var route = db.Routes.ToList().Find(x => x.RouteName == RouteName);
            if (route !=null)
            {

            }

            //if (!String.IsNullOrEmpty(FilterBy))
            //{
            //    if (FilterBy == "Price")
            //    {
            //        name = name.Where(s => s.Price >= Minimum && s.Price <= Maximum);
            //    }

            //}
            return View(name.ToList());
        }
        public ActionResult Index(string searchString, string FilterBy, int? Minimum, int? Maximum, string sortOrder, string CurrentSort, int? page)
        {
            var name = from s in db.Routes
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                name = name.Where(s => s.RouteName.ToUpper().Contains(searchString.ToUpper()));
            }

            int pageSize = 3;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            ViewBag.CurrentSort = sortOrder;

            sortOrder = String.IsNullOrEmpty(sortOrder) ? "RouteId" : sortOrder;

            IPagedList<Route> d = null;

            switch (sortOrder)
            {
                case "RouteId":
                    if (sortOrder.Equals(CurrentSort))
                        d = db.Routes.OrderByDescending
                        (m => m.RouteId).ToPagedList(pageIndex, pageSize);
                    else
                        d = db.Routes.OrderBy
                        (m => m.RouteId).ToPagedList(pageIndex, pageSize);
                    break;
                case "RouteName":
                    if (sortOrder.Equals(CurrentSort))
                        d = db.Routes.OrderByDescending
                        (m => m.RouteName).ToPagedList(pageIndex, pageSize);
                    else
                        d = db.Routes.OrderBy
                        (m => m.RouteName).ToPagedList(pageIndex, pageSize);
                    break;

                case "RouteDistance":
                    if (sortOrder.Equals(CurrentSort))
                        d = db.Routes.OrderByDescending
                        (m => m.RouteDistance).ToPagedList(pageIndex, pageSize);
                    else
                        d = db.Routes.OrderBy
                        (m => m.RouteDistance).ToPagedList(pageIndex, pageSize);
                    break;
                case "RankId":
                    if (sortOrder.Equals(CurrentSort))
                        d = db.Routes.OrderByDescending
                        (m => m.RankId).ToPagedList(pageIndex, pageSize);
                    else
                        d = db.Routes.OrderBy
                        (m => m.RankId).ToPagedList(pageIndex, pageSize);
                    break;

                case "rankmanagerID":
                    if (sortOrder.Equals(CurrentSort))
                        d = db.Routes.OrderByDescending
                        (m => m.rankmanagerID).ToPagedList(pageIndex, pageSize);
                    else
                        d = db.Routes.OrderBy
                        (m => m.rankmanagerID).ToPagedList(pageIndex, pageSize);
                    break;             
            }
          
            TempData["count"] = db.Routes.Count();
            //return View(rs.GetRoutes());
            return View(d);

        }
        public ActionResult Details(int id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (rs.GetRoute(id) != null)
                return View(rs.GetRoute(id));
            else
                return RedirectToAction("Not_Found", "Error");
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
        public JsonResult Update(Route destination)
        {
            var status = false;
            if (destination != null)
            {
                var obj = db.Routes.Where(m => m.RouteId == destination.RouteId).FirstOrDefault();
                obj.RouteDistance = destination.RouteDistance;
                obj.RouteDuration = destination.RouteDuration;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult AddToRoute()
        {
            //ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");
            //ViewBag.rankmanagerID = new SelectList(db.RankManagers, "rankmanagerID", "FirstName");

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddToRoute(Route r, HttpPostedFileBase img_upload)
        //{
        //    ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");

        //    ViewBag.rankmanagerID = new SelectList(db.RankManagers, "rankmanagerID", "FirstName");

        //    byte[] data = null;
        //    data = new byte[img_upload.ContentLength];
        //    img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
        //    r.picture = data;

        //    var route = db.Routes.Where(x => x.RouteName == r.RouteName);
        //    if (route.Count() != 0)
        //    {
        //        ModelState.AddModelError("", "The Route already exists");
        //    }

        //    if (ModelState.IsValid)
        //    {

        //        var name = r.RouteName.Split(';');
        //        var distance = r.RouteDistance.ToString().Split(';');
        //        foreach (var n in name)
        //        {
        //            r.RouteName = n;
        //            rs.AddRoute(r);
        //        }
        //        //foreach (var b in distance)
        //        //{
        //        //    r.RouteDistance = double.Parse(b);
        //        //    db.Routes.Add(r);
        //        //    db.SaveChanges();
        //        //}


        //        return RedirectToAction("Index");
        //    }
        //    return View(r);
        //}

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        //Display File
        public FileStreamResult RenderImage(int id)
        {
            MemoryStream ms = null;

            var item = db.Routes.FirstOrDefault(x => x.RouteId == id);
            if (item != null)
            {
                ms = new MemoryStream(item.picture);
            }
            return new FileStreamResult(ms, item.ImageType);
        }
        public ActionResult Create()
        {
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");
            ViewBag.rankmanagerID = new SelectList(db.RankManagers, "rankmanagerID", "FirstName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Route r, HttpPostedFileBase img_upload)
        {
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");

            ViewBag.rankmanagerID = new SelectList(db.RankManagers, "rankmanagerID", "FirstName");

            //byte[] data = null;
            //data = new byte[img_upload.ContentLength];
            //img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            //r.picture = data;
            if (img_upload != null && img_upload.ContentLength > 0)
            {
                r.ImageType = Path.GetExtension(img_upload.FileName);
                r.picture = ConvertToBytes(img_upload);
            }

            var route = db.Routes.Where(x => x.RouteName == r.RouteName);
            if (route.Count() != 0)
            {
                ModelState.AddModelError("", "The Route already exists");
            }

            if (ModelState.IsValid)
            {
             
                var name = r.RouteName.Split(';');
                //var distance = r.RouteDistance.ToString().Split(';');
                foreach (var n in name)
                {
                    r.RouteName = n;
                    rs.AddRoute(r);
                }
                //foreach (var b in distance)
                //{
                //    r.RouteDistance = double.Parse(b);
                //    db.Routes.Add(r);
                //    db.SaveChanges();
                //}

               
                return RedirectToAction("Index");
            }
            return View(r);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");

            ViewBag.rankmanagerID = new SelectList(db.RankManagers, "rankmanagerID", "FirstName");

            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (rs.GetRoute(id) != null)
                return View(rs.GetRoute(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Route r)
        {

            if (ModelState.IsValid)
            {
                rs.UpdateUpdate(r);
                return RedirectToAction("Index");
            }
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");

            ViewBag.rankmanagerID = new SelectList(db.RankManagers, "rankmanagerID", "FirstName");

            return View(r);
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (rs.GetRoute(id) != null)
                return View(rs.GetRoute(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rs.RemoveRoute(rs.GetRoute(id));
            return RedirectToAction("Index");
        }

     /// <summary>
     /// 
     /// </summary>
     /// <returns></returns>
        public ActionResult GetRoute()
        {
            return View();
        }
        public ActionResult AddCreate()
        {
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");
            ViewBag.rankmanagerID = new SelectList(db.RankManagers, "rankmanagerID", "FirstName");
            return View();
        }
     [HttpPost]
        public JsonResult createStudent(Route std)
        {
            db.Routes.Add(std);
            db.SaveChanges();
            string message = "SUCCESS";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
        public JsonResult getStudent(string id)
        {
            List<Route> routes = new List<Route>();
            routes = db.Routes.ToList();
            return Json(routes, JsonRequestBehavior.AllowGet);
        }

    }
}