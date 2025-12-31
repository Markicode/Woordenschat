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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(b => b.Isbn)
                .HasMaxLength(13);

            builder
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(b => b.Description)
                .HasMaxLength(2000);

            builder
                .HasIndex(b => b.Isbn)
                .IsUnique();

            builder.Property(b => b.PublishedDate)
                .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),
                    v => DateOnly.FromDateTime(v)
                )
                .HasColumnType("date");

            builder
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<BookAuthor>(  
                    j => j
                        .HasOne(ba => ba.Author)
                        .WithMany()
                        .HasForeignKey(ba => ba.AuthorId),
                    j => j
                        .HasOne(ba => ba.Book)
                        .WithMany()
                        .HasForeignKey(ba => ba.BookId),
                    j =>
                    {
                        j.HasKey(ba => new { ba.BookId, ba.AuthorId });
                        j.ToTable("BookAuthors");
                    }
                );
        }

    }
}
