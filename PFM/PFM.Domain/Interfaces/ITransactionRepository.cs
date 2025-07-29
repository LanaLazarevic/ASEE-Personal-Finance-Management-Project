using PFM.Domain.Dtos;
using PFM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        Task<PagedList<Transaction>> GetTransactionsAsync(TransactionQuerySpecification specification);
        Task<bool> ExistsAsync(string id, CancellationToken ct = default);
        Task<List<string>> GetExistingIdsAsync(List<string> ids, CancellationToken ct = default);
        Task<Transaction?> GetByIdAsync(string id, CancellationToken ct = default);
        Task<List<Transaction>> GetForAnalyticsAsync(AnalyticsTransactionQuerySpecification spec, CancellationToken ct = default);
    }
}
