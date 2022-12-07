using MASFlightBookingAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MASFlightBookingAPI
{
    public class MASFlightDbContext : IdentityDbContext
    {
        public MASFlightDbContext(DbContextOptions options) : base(options)
        {

        }
        public MASFlightDbContext() { }
        DbSet<MASFlightBooking> MASFlights { get; set; }
        DbSet<Users> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<Users>();

        }
    }


    public class MASFlightDbContextFactory : IDesignTimeDbContextFactory<MASFlightDbContext>
    {
        public MASFlightDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MASFlightDbContext>();
            optionsBuilder.UseSqlServer("Server = SHAZYPC\\SQLEXPRESS; Database = MASFlightBookingAPI; MultipleActiveResultSets = True; Trusted_Connection = True");
            return new MASFlightDbContext(optionsBuilder.Options);
        }
    }


}
