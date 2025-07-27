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
    public class SplitEntityTypeConfiguration : IEntityTypeConfiguration<Split>
    {
        public void Configure(EntityTypeBuilder<Split> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                     .HasColumnName("id")
                   .UseIdentityColumn()
                   .IsRequired();

            builder.Property(s => s.TransactionId)
                     .HasColumnName("transaction_id")
                   .IsRequired();

            builder.Property(s => s.CatCode)
                   .HasColumnName("catcode")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(s => s.Amount)
                     .HasColumnName("amount")
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();

            builder.HasOne(s => s.Transaction)
                   .WithMany(t => t.Splits)
                   .HasForeignKey(s => s.TransactionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Category)
                   .WithMany(c => c.Splits)
                   .HasForeignKey(s => s.CatCode)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
