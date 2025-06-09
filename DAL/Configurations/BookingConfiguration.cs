using CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder
                .Property(b => b.NumberOfPassengers)
                .IsRequired();
            
            builder
                .HasMany(b => b.Passengers)
                .WithOne(b => b.Booking);

            builder
                .Property(b => b.TotalPrice)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
