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
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder
                .Property(m => m.MembershipNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder
                .HasIndex(m => m.MembershipNumber)
                .IsUnique();

            builder
                .HasMany(m => m.Reservations)
                .WithOne(r => r.Member)
                .HasForeignKey(r => r.MemberId);

            builder
                .HasMany(m => m.Loans)
                .WithOne(l => l.Member)
                .HasForeignKey(l => l.MemberId);

            builder
                .HasIndex(m => m.PersonId)
                .IsUnique();

        }

    }
}