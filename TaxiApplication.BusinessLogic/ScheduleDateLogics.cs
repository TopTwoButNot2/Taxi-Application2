using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
namespace TaxiApplication.BusinessLogics
{
    public class ScheduleDateLogics
    {
        private ApplicationDbContext db;
        public ScheduleDateLogics()
        {
            this.db = new ApplicationDbContext();
        }
        public List<ScheduleDate> GetScheduleDates()
        {
            return db.ScheduleDates.ToList();
        }
        public bool AddDate(ScheduleDate scheduleDate)
        {
            var c = db.ScheduleDates.Where(b => b.DateFrom < b.DateTo).Select(b => b.Count).FirstOrDefault();
            if (c > 0)
            {
                c = db.ScheduleDates.Where(b => b.DateFrom < b.DateTo).Select(b => b.Count).Max();
            }
            else
            {
                c = 0;
            }
            var countMax = db.ScheduleDates.Where(b => b.DateFrom < b.DateTo).Select(b => b.Count).Max();


            var dateTo = db.ScheduleDates.Where(b => b.DateFrom < b.DateTo && b.Count == countMax).Select(b => b.DateTo).FirstOrDefault();

            try
            {
                scheduleDate.Count = c + 1;
                scheduleDate.DateFrom = dateTo.AddDays(1);
                scheduleDate.DateTo = scheduleDate.DateFrom.AddDays(30);
                db.ScheduleDates.Add(scheduleDate);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public bool AddDate2(ScheduleDate scheduleDate)
        {
            var c = db.ScheduleDates.Where(b => b.DateFrom < b.DateTo).Select(b => b.Count).FirstOrDefault();
            if (c > 0)
            {
                c = db.ScheduleDates.Where(b => b.DateFrom < b.DateTo).Select(b => b.Count).Max();
            }
            else
            {
                c = 0;
            }

            try
            {
                scheduleDate.Count = c + 1;
                scheduleDate.DateTo = scheduleDate.DateFrom.AddDays(30);
                db.ScheduleDates.Add(scheduleDate);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public bool Delete(ScheduleDate scheduleDate)
        {
            try
            {
                db.ScheduleDates.Remove(scheduleDate);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ScheduleDate GetScheduleDate(int? ky)
        {
            return db.ScheduleDates.Find(ky);
        }

        public bool Update(ScheduleDate scheduleDate)
        {
            try
            {
                //  db.Entry(scheduleDate).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }




    }
}








