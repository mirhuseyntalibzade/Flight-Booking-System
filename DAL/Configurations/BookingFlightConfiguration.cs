using CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class BookingFlightConfiguration : IEntityTypeConfiguration<BookingFlight>
    {
        public void Configure(EntityTypeBuilder<BookingFlight> builder)
        {
            builder
                .HasKey(bf => new { bf.BookingId, bf.FlightId });

            builder
                .HasOne(bf => bf.Booking)
                .WithMany(b => b.BookingFlights)
                .HasForeignKey(bf => bf.BookingId);

            builder
                .HasOne(bf => bf.Flight)
                .WithMany(f => f.BookingFlights)
                .HasForeignKey(bf => bf.FlightId);
        }
    }
}
