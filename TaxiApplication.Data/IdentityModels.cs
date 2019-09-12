using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaxiApplication.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public byte[] UserPhoto { get; set; }
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


        public virtual DbSet<ReservedList> reservedList { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ReservedTaxi> reservedTaxis { get; set; }
        public virtual DbSet<Taxi> Taxis { get; set; }
        public virtual DbSet<ScheduleDate> ScheduleDates { get; set; }
        public virtual DbSet<TaxiDriver> TaxiDrivers { get; set; }
        
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Fuel> Fuels { get; set; }
      


        public virtual DbSet<Available> Availables { get; set; }

        public virtual DbSet<RankManager> RankManagers{ get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<TaxiPosition> TaxiPosition { get; set; }

        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<StopOver> StopOvers { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<NextOfKin> NextOfKins { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<TaxiMake> TaxiMakes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
       
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<TaxiModel> TaxiModels { get; set; }
 
        public virtual DbSet<RouteViewModel> routeViewModel { get; set; }
        public virtual DbSet<Laggage> laggagees{ get; set; }
        public virtual DbSet<LaggagePrices> LaggagePrice { get; set; }
        public virtual DbSet<LuggagePrice> LaggagePrices { get; set; }
        public virtual DbSet<TaxiHire> TaxiHires { get; set; }

        public virtual DbSet<LaggageSetting> LaggageSettings { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<WalkingPassenger> walkinPassenger { get; set; }
        public virtual DbSet<WalkinNextOfKin> walkinNextOfKin { get; set; }
        public virtual DbSet<LaggagePrice2> laggagePricce2 { get; set; }
        public System.Data.Entity.DbSet<TaxiApplication.Data.PassengerDetails> PassengerDetails { get; set; }

        public System.Data.Entity.DbSet<TaxiApplication.Data.ReservedPassengersViewModel> ReservedPassengersViewModels { get; set; }
        public System.Data.Entity.DbSet<TaxiApplication.Data.WalkinDetails> WalkinDetails { get; set; }
        public System.Data.Entity.DbSet<TaxiApplication.Data.Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<TaxiApplication.Data.Position> Positions { get; set; }

        public System.Data.Entity.DbSet<TaxiApplication.Data.Transmission> Transmissions { get; set; }
        public System.Data.Entity.DbSet<TaxiApplication.Data.Insurance> Insurances { get; set; }

        public System.Data.Entity.DbSet<TaxiApplication.Data.Booking> Bookings { get; set; }
        public System.Data.Entity.DbSet<TaxiApplication.Data.Extra> Extras { get; set; }

        public System.Data.Entity.DbSet<TaxiApplication.Data.RankDetails> RankDetails { get; set; }
        public virtual DbSet<Laggage2> laggage2 { get; set; }

        public virtual DbSet<Reserved> reserved { get; set; }



        public virtual DbSet<ReservedPassengers> ReservedPassengers { get; set; }

        public System.Data.Entity.DbSet<TaxiApplication.Data.ReservedViewModel> ReservedViewModels { get; set; }
    }
}