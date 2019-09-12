using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
    public class LaggagePrice2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LaggagePrice2
        public ActionResult Index()
        {
            var laggagePricce2 = db.laggagePricce2.Include(l => l.season);
            return View(laggagePricce2.ToList());
        }

        // GET: LaggagePrice2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggagePrice2 laggagePrice2 = db.laggagePricce2.Find(id);
            if (laggagePrice2 == null)
            {
                return HttpNotFound();
            }
            return View(laggagePrice2);
        }

        // GET: LaggagePrice2/Create
        public ActionResult Create()
        {
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description");
            return View();
        }

        // POST: LaggagePrice2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LPId,SeasonID,Price,Size")] LaggagePrice2 laggagePrice2)
        {
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", laggagePrice2.SeasonID);

            var sea = db.Seasons.Where(x => x.SeasonID == laggagePrice2.SeasonID);
            var laggaGE = db.laggagePricce2.Where(x => x.Size == laggagePrice2.Size);

            var last = db.laggagePricce2.Where(x => x.SeasonID == laggagePrice2.SeasonID);

            if (ModelState.IsValid)
            {
                db.laggagePricce2.Add(laggagePrice2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "Size is already on the system";
            }

            return View(laggagePrice2);
        }

        // GET: LaggagePrice2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggagePrice2 laggagePrice2 = db.laggagePricce2.Find(id);
            if (laggagePrice2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", laggagePrice2.SeasonID);
            return View(laggagePrice2);
        }

        // POST: LaggagePrice2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LPId,SeasonID,Price,Size")] LaggagePrice2 laggagePrice2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laggagePrice2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", laggagePrice2.SeasonID);
            return View(laggagePrice2);
        }

        // GET: LaggagePrice2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggagePrice2 laggagePrice2 = db.laggagePricce2.Find(id);
            if (laggagePrice2 == null)
            {
                return HttpNotFound();
            }
            return View(laggagePrice2);
        }

        // POST: LaggagePrice2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LaggagePrice2 laggagePrice2 = db.laggagePricce2.Find(id);
            db.laggagePricce2.Remove(laggagePrice2);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
