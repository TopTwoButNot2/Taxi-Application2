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
    public class LuggagePricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LuggagePrices
        public ActionResult Index()
        {
            return View(db.LaggagePrices.ToList());
        }

        // GET: LuggagePrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LuggagePrice luggagePrice = db.LaggagePrices.Find(id);
            if (luggagePrice == null)
            {
                return HttpNotFound();
            }
            return View(luggagePrice);
        }

        // GET: LuggagePrices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LuggagePrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriceID,Luggage,price")] LuggagePrice luggagePrice)
        {
            if (ModelState.IsValid)
            {
                db.LaggagePrices.Add(luggagePrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(luggagePrice);
        }

        // GET: LuggagePrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LuggagePrice luggagePrice = db.LaggagePrices.Find(id);
            if (luggagePrice == null)
            {
                return HttpNotFound();
            }
            return View(luggagePrice);
        }

        // POST: LuggagePrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceID,Luggage,price")] LuggagePrice luggagePrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(luggagePrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(luggagePrice);
        }

        // GET: LuggagePrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LuggagePrice luggagePrice = db.LaggagePrices.Find(id);
            if (luggagePrice == null)
            {
                return HttpNotFound();
            }
            return View(luggagePrice);
        }

        // POST: LuggagePrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LuggagePrice luggagePrice = db.LaggagePrices.Find(id);
            db.LaggagePrices.Remove(luggagePrice);
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
