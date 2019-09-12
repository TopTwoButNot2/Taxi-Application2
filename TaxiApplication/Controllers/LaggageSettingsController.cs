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
    public class LaggageSettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LaggageSettings
        public ActionResult Index()
        {
            return View(db.LaggageSettings.ToList());
        }

        // GET: LaggageSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggageSetting laggageSetting = db.LaggageSettings.Find(id);
            if (laggageSetting == null)
            {
                return HttpNotFound();
            }
            return View(laggageSetting);
        }

        // GET: LaggageSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LaggageSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "laggID,NumOfBags,Size,Test")] LaggageSetting laggageSetting)
        {
            if (ModelState.IsValid)
            {
                db.LaggageSettings.Add(laggageSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laggageSetting);
        }

        // GET: LaggageSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggageSetting laggageSetting = db.LaggageSettings.Find(id);
            if (laggageSetting == null)
            {
                return HttpNotFound();
            }
            return View(laggageSetting);
        }

        // POST: LaggageSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "laggID,NumOfBags,Size,Test")] LaggageSetting laggageSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laggageSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laggageSetting);
        }

        // GET: LaggageSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaggageSetting laggageSetting = db.LaggageSettings.Find(id);
            if (laggageSetting == null)
            {
                return HttpNotFound();
            }
            return View(laggageSetting);
        }

        // POST: LaggageSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LaggageSetting laggageSetting = db.LaggageSettings.Find(id);
            db.LaggageSettings.Remove(laggageSetting);
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
