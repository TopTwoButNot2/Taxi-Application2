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
    public class RankManagersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RankManagers
        public ActionResult Index()
        {
            TempData["count"] = db.RankManagers.Count();
            var rankManagers = db.RankManagers.Include(r => r.Rank);
            return View(rankManagers.ToList());
        }

        // GET: RankManagers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankManager rankManager = db.RankManagers.Find(id);
            if (rankManager == null)
            {
                return HttpNotFound();
            }
            return View(rankManager);
        }

        // GET: RankManagers/Create
        public ActionResult Create()
        {
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName");
            return View();
        }

        // POST: RankManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rankmanagerID,FirstName,LastName,Gender,PhoneNumber,Address,Email,RankId")] RankManager rankManager)
        {

            var rankM = db.RankManagers.Where(x => x.Email == rankManager.Email);
            if (rankM.Count() != 0)
            {
                ModelState.AddModelError("", "The RankManager already exists");
            }

            if (ModelState.IsValid)
            {
                rankManager.rankmanagerID = Guid.NewGuid().ToString();
                db.RankManagers.Add(rankManager);
                db.SaveChanges();

                EmailService es = new EmailService();
                es.SendRegMail(rankManager.Email, "YMCA Account Activation", "Rank Manager");
                return RedirectToAction("Index");
            }

            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName", rankManager.RankId);
            return View(rankManager);
        }

        // GET: RankManagers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankManager rankManager = db.RankManagers.Find(id);
            if (rankManager == null)
            {
                return HttpNotFound();
            }
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName", rankManager.RankId);
            return View(rankManager);
        }

        // POST: RankManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rankmanagerID,FirstName,LastName,Gender,PhoneNumber,Address,Email,RankId")] RankManager rankManager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rankManager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName", rankManager.RankId);
            return View(rankManager);
        }

        // GET: RankManagers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankManager rankManager = db.RankManagers.Find(id);
            if (rankManager == null)
            {
                return HttpNotFound();
            }
            return View(rankManager);
        }

        // POST: RankManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RankManager rankManager = db.RankManagers.Find(id);
            db.RankManagers.Remove(rankManager);
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
