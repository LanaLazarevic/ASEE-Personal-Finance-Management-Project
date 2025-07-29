using Microsoft.EntityFrameworkCore;
using PFM.Domain.Dtos;
using PFM.Domain.Entities;
using PFM.Domain.Interfaces;
using PFM.Infrastructure.Persistence.DbContexts;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PFM.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PFMDbContext _ctx;


        public CategoryRepository(PFMDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(Category category)
        {
            _ctx.Categories.Add(category);
        }

        public async Task<List<Category>> GetByCodesAsync(IEnumerable<string> codes, CancellationToken ct = default)
        {
            return await _ctx.Categories
                         .Where(c => codes.Contains(c.Code))
                         .ToListAsync(ct);
        }

        public async Task<List<Category>> GetAll(string? code, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(code))
                return await _ctx.Categories.ToListAsync(ct);
            else
                return await _ctx.Categories.Where(c => c.ParentCode == code)
                         .ToListAsync(ct);
        }
    }
}
