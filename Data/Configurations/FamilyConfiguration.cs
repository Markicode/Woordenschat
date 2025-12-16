using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class FamilyConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder
                .HasMany(f => f.Members)
                .WithOne(m => m.Family)
                .HasForeignKey(m => m.FamilyId);

            builder
                .Property(f => f.FamilyName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(f => f.Adress)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(f => f.PostalCode)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(f => f.City)
                .IsRequired()
                .HasMaxLength(100);

        }

    }
}