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
    public class AuthConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            
            builder
                .Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(u => u.Gender)
                .IsRequired();
            
            builder
                .Property(u => u.DOB)
                .IsRequired();

            builder
                .Property(u => u.UserName)
                .HasMaxLength(50)
                .IsRequired();
            
            builder
                .Property(u => u.Email)
                .IsRequired();
        }
    }
}
