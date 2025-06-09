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
    public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(a => a.IATA)
                .IsRequired()
                .HasMaxLength(3);

            builder
                .Property(a => a.ICAO)
                .IsRequired()
                .HasMaxLength(4);

            builder
                .HasOne(a => a.Country)
                .WithMany(a => a.Airlines)
                .HasForeignKey(a => a.CountryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(a => a.LogoUrl)
                .IsRequired();

            builder
                .HasMany(a => a.Flights)
                .WithOne(a => a.Airline);

            builder
                .HasMany(a => a.Aircrafts)
                .WithOne(a => a.Airline);

        }
    }
}
