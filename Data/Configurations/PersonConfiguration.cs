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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Persons)
                .HasForeignKey(p => p.UserId);

            builder
                .HasOne(p => p.Member)
                .WithOne(m => m.Person)
                .HasForeignKey<Member>(m => m.PersonId);

            builder
                .HasOne(p => p.Employee)
                .WithOne(e => e.Person)
                .HasForeignKey<Employee>(e => e.PersonId);

        }

    }
}