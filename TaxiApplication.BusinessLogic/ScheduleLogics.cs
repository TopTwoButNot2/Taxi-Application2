using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class ScheduleLogics
    {
        private ApplicationDbContext db;
        public ScheduleLogics()
        {
            this.db = new ApplicationDbContext();
        }

        public List<Schedule> GetSchedules()
        {
            return db.Schedules.Include(mww => mww.owner).Include(rr => rr.rank).Include(rr => rr.route).Include(rr => rr.ScheduleDate).ToList();
        }

        public bool Add(Schedule schedule)
        {

            var po = db.Schedules.Where(n => n.RankId == schedule.RankId && n.RouteId == schedule.RouteId && n.id == schedule.id).Select(n => n.Position).FirstOrDefault();
            if (po > 0)
            {
                po = db.Schedules.Where(n => n.RankId == schedule.RankId && n.RouteId == schedule.RouteId && n.id == schedule.id).Select(n => n.Position).Max();
            }
            else
            {
                po = 0;
            }


            try
            {
                var routid = db.Routes.Where(x => x.RouteId == schedule.RouteId).Select(x => x.RouteName).FirstOrDefault();



                schedule.Ss = schedule.SelectSeason();
                schedule.Routeee = routid;
                schedule.Position = po + 1;
                schedule.RoutePrice = schedule.SelectPrice();
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(Schedule schedule)
        {
            try
            {
                db.Schedules.Remove(schedule);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Schedule GetSchedule(int? No)
        {

            var schedule = db.Schedules.ToList().Find(x=> x.No == No);

            return schedule;
        }
        public List<Schedule> schedulList(string email)
        {
            List<TaxiPosition> TaxiPositions = db.TaxiPosition.ToList();
            List<Schedule> schedule = new List<Schedule>();
            var owner = db.Owners.ToList().Find(n => n.Email == email);
            foreach (var x in db.Schedules)
            {
                if (x.ownerID == owner.ownerID)
                {
                    schedule.Add(x);
                }

            }

            foreach (var x in db.Schedules)
            {
                foreach (var e in TaxiPositions)
                {
                    if (e.No == x.No)
                    {
                        schedule.Remove(x);
                    }
                }

            }
            return schedule;
        }

    }
}
