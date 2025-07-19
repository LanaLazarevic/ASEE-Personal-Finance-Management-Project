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
                   .IsRequired();

            
            builder.Property(t => t.BeneficiaryName)
                   .HasColumnName("beneficiary_name")
                   .HasMaxLength(200)
                   .IsRequired(false);

          
            builder.Property(t => t.Date)
                   .IsRequired();

           
            builder.Property(t => t.Direction)
                   .IsRequired();

            builder.Property(t => t.Amount)
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();

           
            builder.Property(t => t.Description)
                   .HasMaxLength(500)
                   .IsRequired(false);

           
            builder.Property(t => t.Currency)
                   .HasMaxLength(3)
                   .IsFixedLength()    
                   .IsRequired();

          
            builder.Property(t => t.Mcc)
                   .HasConversion<int>()
                   .HasColumnType("integer")
                   .IsRequired(false);

          
            builder.Property(t => t.Kind)
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
