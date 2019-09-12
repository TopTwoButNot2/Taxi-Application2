using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;

namespace TaxiApplication.BusinessLogics
{
    public class RouteServices
    {
        private ApplicationDbContext db;
        public RouteServices()
        {
            this.db = new ApplicationDbContext();
        }

        public List<Route> GetRoutes()
        {
            return db.Routes.Include(r => r.Rank).Include(r => r.rankmanager).ToList();
        }

        public bool AddRoute(Route route)
        {

            try
            {

                db.Routes.Add(route);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateUpdate(Route route)
        {
            try
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveRoute(Route route)
        {
            try
            {
                db.Routes.Remove(route);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Route GetRoute(int route_Id)
        {
            return db.Routes.Find(route_Id);
        }
    }
}