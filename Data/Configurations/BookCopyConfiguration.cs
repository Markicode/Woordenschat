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
    public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookId);

            builder
                .Property(bc => bc.Condition)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(bc => bc.InventoryNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasIndex(bc => bc.InventoryNumber)
                .IsUnique();

        }

    }
}