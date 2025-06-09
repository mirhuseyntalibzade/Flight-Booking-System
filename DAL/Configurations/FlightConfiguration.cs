using CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {

            builder
                .HasOne(f => f.Airline)
                .WithMany(f => f.Flights)
                .HasForeignKey(f => f.AirlineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(f => f.Aircraft)
                .WithMany(f => f.Flights)
                .HasForeignKey(f => f.AircraftId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(f => f.Seats)
                .WithOne(f => f.Flight);

            builder
                .Property(f => f.FlightNumber)
                .IsRequired();

            builder
                .Property(f => f.Origin)
                .IsRequired();
            
            builder
                .Property(f => f.Destination)
                .IsRequired();
            
            builder
                .Property(f => f.DepartureTime)
                .IsRequired();
            
            builder
                .Property(f => f.ArrivalTime)
                .IsRequired();
            
            builder
                .Property(f => f.Price)
                .HasPrecision(18,2)
                .IsRequired();
        }
    }
}
