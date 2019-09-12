using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaxiApplication.Models;
namespace TaxiApplication.Models
{
    public class RankLogic
    {
        private ApplicationDbContext db;

        public RankLogic()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Rank> GetRanks()
        {
            return db.Ranks.ToList();
        }
        public bool AddRank(Rank rank)
        {
            try
            {
                db.Ranks.Add(rank);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool UpdateRanks(Rank rank)
        {
            try
            {
                db.Entry(rank).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool RemoveRank(Rank rank)
        {
            try
            {
                db.Ranks.Remove(rank);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Rank GetRank(int? rank_id)
        {
            return db.Ranks.Find(rank_id);
        }

    }
}