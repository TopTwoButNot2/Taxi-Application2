using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaxiApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }



        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<RankManager> RankManagers{ get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<TaxiPosition> TaxiPosition { get; set; }

        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<StopOver> StopOvers { get; set; }
        public virtual DbSet<Taxi> Taxis { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<NextOfKin> NextOfKins { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<TaxiMake> TaxiMakes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<TaxiModel> TaxiModels { get; set; }
        public virtual DbSet<RouteViewModel> routeViewModel { get; set; }





        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}