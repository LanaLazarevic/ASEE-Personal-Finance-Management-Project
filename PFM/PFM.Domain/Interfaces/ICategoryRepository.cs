using PFM.Domain.Dtos;
using PFM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetByCodesAsync(IEnumerable<string> codes, CancellationToken ct = default);

        Task<List<Category>> GetAll(string? code, CancellationToken ct = default);

        void Add(Category category);
    }
}
