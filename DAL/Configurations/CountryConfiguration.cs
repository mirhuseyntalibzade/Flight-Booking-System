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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .Property(b => b.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(b => b.ISOCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .IsRequired();
            builder
               .HasMany(b => b.Airlines)
               .WithOne(b => b.Country);
        }
    }
}
