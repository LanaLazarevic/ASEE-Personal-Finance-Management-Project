using Microsoft.EntityFrameworkCore;
using PFM.Domain.Dtos;
using PFM.Domain.Entities;
using PFM.Domain.Interfaces;
using PFM.Infrastructure.Persistence.DbContexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PFMDbContext _ctx;

        public TransactionRepository(PFMDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(Transaction transaction)
        {
            _ctx.Transactions.Add(transaction);
        }

        public async Task<PagedList<Transaction>> GetTransactionsAsync(TransactionQuerySpecification spec)
        {
            var query = _ctx.Transactions.Include(t=>t.Splits).AsQueryable();

            if (spec.StartDate.HasValue)
                query = query.Where(t => t.Date >= spec.StartDate.Value);

            if (spec.EndDate.HasValue)
                query = query.Where(t => t.Date <= spec.EndDate.Value);

            if (spec.Kind != null && spec.Kind.Any())
                query = query.Where(t => spec.Kind.Contains(t.Kind));


            query = spec.SortBy.ToLower() switch
            {
                "id" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Id)
                    : query.OrderByDescending(t => t.Id),

                "beneficiaryname" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.BeneficiaryName)
                    : query.OrderByDescending(t => t.BeneficiaryName),

                "date" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Date)
                    : query.OrderByDescending(t => t.Date),

                "direction" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Direction.ToString())
                    : query.OrderByDescending(t => t.Direction.ToString()),

                "amount" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Amount)
                    : query.OrderByDescending(t => t.Amount),

                "description" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Description)
                    : query.OrderByDescending(t => t.Description),

                "currency" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Currency)
                    : query.OrderByDescending(t => t.Currency),

                "mcc" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Mcc.HasValue ? t.Mcc.ToString() : "")
                    : query.OrderByDescending(t => t.Mcc.HasValue ? t.Mcc.ToString() : ""),

                "kind" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Kind.ToString())
                    : query.OrderByDescending(t => t.Kind.ToString()),

                "catcode" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.CatCode)
                    : query.OrderByDescending(t => t.CatCode),

                _ => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Date)
                    : query.OrderByDescending(t => t.Date)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((spec.Page - 1) * spec.PageSize)
                .Take(spec.PageSize)
                .ToListAsync();

            return new PagedList<Transaction>()
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = spec.PageSize,
                Page = spec.Page,
                SortBy = spec.SortBy,
                SortOrderd = spec.SortOrder.ToString(),
                TotalPages = (int)Math.Ceiling((double)totalCount / spec.PageSize)
            };
        }

        public async Task<bool> ExistsAsync(string id, CancellationToken ct = default)
        {
            return await _ctx.Transactions.AnyAsync(t => t.Id == id, ct);
        }
        public async Task<List<string>> GetExistingIdsAsync(List<string> ids, CancellationToken ct = default)
        {
            return await _ctx.Transactions
                        .Where(t => ids.Contains(t.Id))
                        .Select(t => t.Id)
                        .ToListAsync(ct);
        }

        public async Task<Transaction?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await _ctx.Transactions
                         .Include(t => t.Category)
                         .FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<List<Transaction>> GetForAnalyticsAsync(AnalyticsTransactionQuerySpecification spec, CancellationToken ct = default)
        {
            var q = _ctx.Transactions
                        .Include(t => t.Category)
                        .Include(t => t.Splits!)
                           .ThenInclude(s => s.Category)
                        .AsQueryable();

            if (spec.StartDate.HasValue)
                q = q.Where(t => t.Date >= spec.StartDate.Value.Date);

            if (spec.EndDate.HasValue)
                q = q.Where(t => t.Date <= spec.EndDate.Value.Date);

            if (spec.Direction.HasValue)
                q = q.Where(t => t.Direction == spec.Direction.Value);

            return await q.ToListAsync(ct);
        }

    }
}
