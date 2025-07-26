using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PFM.Application.Interfaces;
using PFM.Domain.Dtos;
using PFM.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Infrastructure.Services
{
    public class AutoCategorizationService : IAutoCategorizationService
    {
        private readonly PFMDbContext _context;
        private readonly IConfiguration _configuration;

        public AutoCategorizationService(PFMDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> AutoCategorizeTransactionsAsync(List<CategorizationRule> rules, CancellationToken cancellationToken = default)
        {

            int totalAffected = 0;

            foreach (var rule in rules)
            {
                if (!CheckSql(rule.Predicate))
                {
                    return -1;
                }

                string sql = $@"
                    UPDATE ""PFM"".""Transactions""
                    SET catcode = @code
                    WHERE catcode IS NULL AND ({rule.Predicate})
                ";

                totalAffected += await _context.Database.ExecuteSqlRawAsync(sql,
                    new NpgsqlParameter("@code", rule.Code));
            }

            return totalAffected;
        }

        private bool CheckSql(string predicate)
        {
            var lowered = predicate.ToLowerInvariant();

            string[] disallowed = { "drop", "delete", "insert", "update", ";", "--", "truncate", "alter" };

            foreach (var word in disallowed)
            {
                if (lowered.Contains(word))
                    return false;
            }

            return true;
        }
    }
}
