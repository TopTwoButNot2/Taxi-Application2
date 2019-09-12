using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;
 

namespace TaxiApplication.Controllers
{
    public class LaggagePricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LaggagePrices
        public ActionResult Index()
        {
            var laggagePrice = db.LaggagePrice.Include(l => l.ls);
            return View(laggagePrice.ToList());
        }

        // GET: LaggagePrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggagePrices laggagePrices = db.LaggagePrice.Find(id);
            if (laggagePrices == null)
            {
                return HttpNotFound();
            }
            return View(laggagePrices);
        }

        // GET: LaggagePrices/Create
        public ActionResult Create()
        {
            ViewBag.laggID = new SelectList(db.LaggageSettings, "laggID", "Size");
            return View();
        }

        // POST: LaggagePrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LagID,price,laggID")] LaggagePrices laggagePrices)
        {
            if (ModelState.IsValid)
            {
                db.LaggagePrice.Add(laggagePrices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.laggID = new SelectList(db.LaggageSettings, "laggID", "Size", laggagePrices.laggID);
            return View(laggagePrices);
        }

        // GET: LaggagePrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggagePrices laggagePrices = db.LaggagePrice.Find(id);
            if (laggagePrices == null)
            {
                return HttpNotFound();
            }
            ViewBag.laggID = new SelectList(db.LaggageSettings, "laggID", "Size", laggagePrices.laggID);
            return View(laggagePrices);
        }

        // POST: LaggagePrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LagID,price,laggID")] LaggagePrices laggagePrices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laggagePrices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.laggID = new SelectList(db.LaggageSettings, "laggID", "Size", laggagePrices.laggID);
            return View(laggagePrices);
        }

        // GET: LaggagePrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggagePrices laggagePrices = db.LaggagePrice.Find(id);
            if (laggagePrices == null)
            {
                return HttpNotFound();
            }
            return View(laggagePrices);
        }

        // POST: LaggagePrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LaggagePrices laggagePrices = db.LaggagePrice.Find(id);
            db.LaggagePrice.Remove(laggagePrices);
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
