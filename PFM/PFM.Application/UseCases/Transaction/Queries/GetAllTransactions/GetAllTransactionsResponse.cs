using PFM.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Queries.GetAllTransactions
{
    public class GetAllTransactionsResponse
    {
        public PagedList<TransactionDto> Transactions { get; set; }
    }
}
