using CORE.Enums;
using CORE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Reflection;

namespace DAL.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        private readonly IConfiguration _configuration;
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatClass> SeatClasses { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public AppDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> opt) : base(opt)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            #region Role

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "897883a6-438e-4710-8224-0066485fa2b7", Name = Role.Admin.ToString(), NormalizedName = Role.Admin.ToString().ToUpper() },
                new IdentityRole { Id = "b4e2b8fd-5b95-4679-ac72-dc6db51257f8", Name = Role.Manager.ToString(), NormalizedName = Role.Manager.ToString().ToUpper() },
                new IdentityRole { Id = "b67f8d17-ca53-4b68-bdaa-67c965d09308", Name = Role.User.ToString(), NormalizedName = Role.User.ToString().ToUpper() }
                );

            #endregion

            #region Admin

            AppUser admin = new AppUser
            {
                Id = "458b8206-7801-4e07-b9d7-1567e5adc716",
                UserName = _configuration["AdminCredentials:UserName"],
                NormalizedUserName = _configuration["AdminCredentials:UserName"].ToUpper(CultureInfo.GetCultureInfo("en")),
                Email = _configuration["AdminCredentials:Email"],
                NormalizedEmail = _configuration["AdminCredentials:Email"].ToUpper(CultureInfo.GetCultureInfo("en")),
                FirstName = _configuration["AdminCredentials:FirstName"],
                LastName = _configuration["AdminCredentials:LastName"],
                Gender = Gender.Male,
                DOB = DateTime.Now
            };

            PasswordHasher<AppUser> hasher = new();

            admin.PasswordHash = hasher.HashPassword(admin, _configuration["AdminCredentials:Password"]);

            modelBuilder.Entity<AppUser>().HasData(admin);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "897883a6-438e-4710-8224-0066485fa2b7", UserId = "458b8206-7801-4e07-b9d7-1567e5adc716" }
                );

            #endregion
        }
    }
}