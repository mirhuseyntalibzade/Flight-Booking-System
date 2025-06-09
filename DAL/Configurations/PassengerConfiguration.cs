using CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder
                .HasOne(p => p.Booking)
                .WithMany(p => p.Passengers)
                .HasForeignKey(p => p.BookingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            
            builder
                .Property(p => p.Surname)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.DOB)
                .IsRequired();

            builder
                .Property(p => p.PassportNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .HasOne(p => p.Seat)
                .WithOne(p => p.Passenger)
                .HasForeignKey<Seat>(p=>p.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
