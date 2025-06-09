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
    public class SeatClassConfiguration : IEntityTypeConfiguration<SeatClass>
    {
        public void Configure(EntityTypeBuilder<SeatClass> builder)
        {
            builder
                .HasOne(s => s.Aircraft)
                .WithMany(s => s.SeatClasses)
                .HasForeignKey(s => s.AircraftId)
                .IsRequired();

            builder
               .Property(s => s.StartingRow)
               .IsRequired();

            builder
                .Property(s => s.EndingRow)
                .IsRequired();

            builder
                .Property(s => s.ClassName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(s => s.Columns)
                .IsRequired();
            
            builder
                .Property(s => s.AutoAssign)
                .IsRequired();
        }
    }
}
