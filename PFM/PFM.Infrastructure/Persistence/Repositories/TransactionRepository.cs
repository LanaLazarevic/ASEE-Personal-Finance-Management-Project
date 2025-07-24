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
        private readonly IConfigurationProvider _mapperConfig;

        public TransactionRepository(PFMDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapperConfig = mapper.ConfigurationProvider;
        }
        public void Add(Transaction transaction)
        {
            _ctx.Transactions.Add(transaction);
        }

        public async Task<PagedList<TransactionDto>> GetTransactionsAsync(TransactionQuerySpecification spec)
        {
            var query = _ctx.Transactions.AsQueryable();

            if (spec.StartDate.HasValue)
                query = query.Where(t => t.Date >= spec.StartDate.Value);

            if (spec.EndDate.HasValue)
                query = query.Where(t => t.Date <= spec.EndDate.Value);

            if (spec.Kind != null && spec.Kind.Any())
                query = query.Where(t => spec.Kind.Contains(t.Kind));

            query = spec.SortBy.ToLower() switch
            {
                "amount" => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Amount)
                    : query.OrderByDescending(t => t.Amount),
                _ => spec.SortOrder == SortOrder.Asc
                    ? query.OrderBy(t => t.Date)
                    : query.OrderByDescending(t => t.Date)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((spec.Page - 1) * spec.PageSize)
                .Take(spec.PageSize)
                .ProjectTo<TransactionDto>(_mapperConfig)
                .ToListAsync();

            return new PagedList<TransactionDto>()
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
    }
}
