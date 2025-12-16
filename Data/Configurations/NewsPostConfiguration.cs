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
    public class NewsPostConfiguration : IEntityTypeConfiguration<NewsPost>
    {
        public void Configure(EntityTypeBuilder<NewsPost> builder)
        {
            builder
                .HasOne(np => np.CreatedByEmployee)
                .WithMany(e => e.CreatedNewsPosts)
                .HasForeignKey(np => np.CreatedByEmployeeId);

            builder
                .Property(np => np.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(np => np.Content)
                .IsRequired()
                .HasMaxLength(5000);

        }

    }
}