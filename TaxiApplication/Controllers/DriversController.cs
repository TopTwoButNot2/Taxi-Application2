using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;
using System.Data.Entity;

namespace TaxiApplication.Controllers
{
    public class DriversController : Controller
    {
        private ApplicationDbContext db;
        private DriverLogics logic;
        private OwnerLogics Ownerlogic;

        public DriversController()
        {
            this.logic = new DriverLogics();
            this.Ownerlogic = new OwnerLogics();
            this.db = new ApplicationDbContext();
        }

        // GET: Drivers
        public ActionResult Index()
        {
            var drivers = db.Drivers;
            var Owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
            TempData["count"] = drivers.ToList().Where(x => x.ownerID == Owner.ownerID).Count();
            return View(drivers.ToList().Where(x => x.ownerID == Owner.ownerID));
        }
        public ActionResult Create()
        {
            ViewBag.ownerID = new SelectList(Ownerlogic.GetOwners(), "ownerID", "FirstName");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public JsonResult Create(Driver driver)
        {
            var Driver = db.Drivers.Where(x => x.Email == driver.Email);
            if (Driver.Count() != 0)
            {
                ModelState.AddModelError("", "The Driver already exists");
            }
            if (ModelState.IsValid)
            {
                string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                int month = driver.PDP.Month;
                Session["date"] = months[month - 1];
                Session["day"] = driver.PDP.Day;

                Session["time"] = driver.PDP.TimeOfDay.ToString().Replace("PM", "");

                var Owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
                driver.ownerID = Owner.ownerID;
                logic.AddDriver(driver);
                EmailService es = new EmailService();
                es.SendRegMail(driver.Email, "YMCA Account Activation", "Taxi Driver");

                return Json("/Drivers/Index", JsonRequestBehavior.AllowGet);
            }
            return Json("An error occured, please try again", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendEmail(string Subject)
        {
            try
            {
                var lead = db.Drivers.ToList().Find(x => x.driverID == Session["driverID"].ToString());
                EmailService es = new EmailService();
                es.SendRegMaill(lead.Email,Subject, lead.driverID);
                Session["time"] = "";
                Session["date"] = "";
                Session["day"] = "";
            }
            catch (NullReferenceException)
            {

            }


            //initialize reminder
            return RedirectToAction("Driver", "Home");
        }
        public JsonResult getDriver(string driverId)
        {
            return Json(db.Drivers.FirstOrDefault(x => x.driverID == driverId), JsonRequestBehavior.AllowGet);
        }
        [HttpPatch]
        public JsonResult getQR(string email)
        {
            ReservedList rl = new ReservedList();
            rl.Status = "On the taxi";
            db.Entry(rl).State = EntityState.Modified;
            db.SaveChanges();
            TaxiPosition t = new TaxiPosition();
            t.ReservedCount = +1;
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
            return Json(email, JsonRequestBehavior.AllowGet);
        }
    }
}