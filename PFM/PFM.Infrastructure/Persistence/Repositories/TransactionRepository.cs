using PFM.Domain.Entities;
using PFM.Domain.Interfaces;
using PFM.Infrastructure.Persistence.DbContexts;
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
    }
}
