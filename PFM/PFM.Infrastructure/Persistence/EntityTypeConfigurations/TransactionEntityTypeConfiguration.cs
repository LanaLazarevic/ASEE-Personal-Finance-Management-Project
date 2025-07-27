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
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            
            builder.Property(t => t.Id)
                .HasColumnName("id")
                   .IsRequired();

            
            builder.Property(t => t.BeneficiaryName)
                   .HasColumnName("beneficiary_name")
                   .HasMaxLength(200)
                   .IsRequired(false);

          
            builder.Property(t => t.Date)
                     .HasColumnName("date")
                   .IsRequired();

           
            builder.Property(t => t.Direction)
                .HasColumnName("direction")
                   .IsRequired();

            builder.Property(t => t.Amount)
                     .HasColumnName("amount")
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();

           
            builder.Property(t => t.Description)
                     .HasColumnName("description")
                   .HasMaxLength(500)
                   .IsRequired(false);

           
            builder.Property(t => t.Currency)
                     .HasColumnName("currency")
                   .HasMaxLength(3)
                   .IsFixedLength()    
                   .IsRequired();

          
            builder.Property(t => t.Mcc)
                     .HasColumnName("mcc")
                   .HasConversion<int>()
                   .HasColumnType("integer")
                   .IsRequired(false);

          
            builder.Property(t => t.Kind)
                     .HasColumnName("kind")
                   .IsRequired();

     
            builder.Property(t => t.CatCode)
                   .HasColumnName("catcode")
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.HasOne(t => t.Category)
                   .WithMany(c => c.Transactions)
                   .HasForeignKey(t => t.CatCode)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
