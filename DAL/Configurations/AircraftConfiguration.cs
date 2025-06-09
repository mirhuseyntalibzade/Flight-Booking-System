using CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(a => a.Manufacturer)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(a => a.Capacity)
                .IsRequired()
                .HasDefaultValue(100);

            builder
                .HasOne(a => a.Airline)
                .WithMany(a => a.Aircrafts)
                .HasForeignKey(a => a.AirlineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
                
            builder
                .HasMany(a => a.Flights)
                .WithOne(a => a.Aircraft);
            
            builder
                .HasMany(a => a.SeatClasses)
                .WithOne(a => a.Aircraft);
        }
    }
}
