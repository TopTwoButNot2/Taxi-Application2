using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;


namespace TaxiApplication.Controllers
{
    public class PricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prices
        public ActionResult Index()
        {
            TempData["count"] = db.Prices.Count();
            var prices = db.Prices.Include(p => p.Route);
            return View(prices.ToList());
        }

        // GET: Prices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // GET: Prices/Create
        public ActionResult Create()
        {
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName");
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description");

            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriceID,pricevalue,RouteId,SeasonID")] Price price)
        {
            var prices = db.Prices.Where(x => x.RouteId == price.RouteId && x.SeasonID==price.SeasonID);
            if (prices.Count() != 0)
            {
                TempData["AlertMessage"] = "The Price and Season already Captured";
                ModelState.AddModelError("", "");
            }
            if (ModelState.IsValid)
            {
                price.Routeee = price.SelectRouteName();
                price.DateF = price.SelectStartDates();
                price.DateT = price.SelectEndDates();
                price.Seannnn = price.SelectDescription();
                db.Prices.Add(price);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", price.SeasonID);

            //ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", price.RouteId);
            return View(price);
        }

        // GET: Prices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", price.SeasonID);

            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", price.RouteId);
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceID,price,RouteId,SeasonID")] Price price)
        {
            if (ModelState.IsValid)
            {
                db.Entry(price).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", price.RouteId);
            return View(price);
        }

        // GET: Prices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Price price = db.Prices.Find(id);
            db.Prices.Remove(price);
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
