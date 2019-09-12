using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;


namespace TaxiApplication.BusinessLogics
{
    public class SeasonServices
    {
        private ApplicationDbContext db;
        public SeasonServices()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Season> GetSeasons()
        {
            return db.Seasons.ToList();
        }
        public bool AddSeason(Season season)
        {
            try
            {

                db.Seasons.Add(season);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool UpdateSeason(Season season)
        {
            try
            {
                db.Entry(season).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveSeason(Season season)
        {
            try
            {
                db.Seasons.Remove(season);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Season GetSeason(int? season_Id)
        {
            return db.Seasons.Find(season_Id);
        }
    }
}