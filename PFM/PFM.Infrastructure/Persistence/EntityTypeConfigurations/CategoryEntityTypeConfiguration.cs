using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Code);

            builder.Property(c => c.Code)
                     .HasColumnName("code")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Name)
                     .HasColumnName("name")
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.ParentCode)
                    .HasColumnName("parent_code")
                   .HasMaxLength(20)
                    .IsRequired(false);

            builder.HasOne(c => c.Parent)
                   .WithMany(p => p.Children)
                   .HasForeignKey(c => c.ParentCode)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
