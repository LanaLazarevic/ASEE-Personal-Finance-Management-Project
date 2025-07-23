using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Infrastructure.Persistence.DbContexts
{
    public class PFMDbContextFactory : IDesignTimeDbContextFactory<PFMDbContext>
    {
        public PFMDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PFMDbContext>();

            var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
            if (string.IsNullOrEmpty(host))
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                builder.UseNpgsql(config.GetConnectionString("Default"));
            }
            else
            {
                var port = Environment.GetEnvironmentVariable("DATABASE_PORT");
                var dbName = Environment.GetEnvironmentVariable("DATABASE_NAME");
                var user = Environment.GetEnvironmentVariable("DATABASE_USER");
                var password = Environment.GetEnvironmentVariable("DATABASE_PASS");
                var conn = $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";
                builder.UseNpgsql(conn);
            }

            return new PFMDbContext(builder.Options);
        }

    }
}
