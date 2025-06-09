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
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder
                .Property(s => s.FlightId)
                .IsRequired();

            builder
                .HasOne(s => s.Passenger)
                .WithOne(p => p.Seat)
                .HasForeignKey<Seat>(s => s.PassengerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(s => s.SeatNumber)
                .HasMaxLength(5)
                .IsRequired();

            builder
                .Property(s => s.SeatClass)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(s => s.AutoAssign)
                .IsRequired();
        }
    }
}
