using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFM.Domain.Entities;
using PFM.Infrastructure.Persistence.EntityTypeConfigurations;


namespace PFM.Infrastructure.Persistence.DbContexts
{
    public class PFMDbContext : DbContext
    {
        public PFMDbContext(DbContextOptions<PFMDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Split> Splits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
                var port = Environment.GetEnvironmentVariable("DATABASE_PORT");
                var dbName = Environment.GetEnvironmentVariable("DATABASE_NAME");
                var user = Environment.GetEnvironmentVariable("DATABASE_USER");
                var password = Environment.GetEnvironmentVariable("DATABASE_PASS");

                var conn = $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";
                optionsBuilder.UseNpgsql(conn);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PFM");
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SplitEntityTypeConfiguration());
        }
        
     
    }
}
