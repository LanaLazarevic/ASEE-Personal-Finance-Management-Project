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
        Task<PagedList<TransactionDto>> GetTransactionsAsync(TransactionQuerySpecification specification);
    }
}
