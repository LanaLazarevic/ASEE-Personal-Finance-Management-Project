using PFM.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.Result
{
    public class GetAllTransactionsResponse
    {
        public required PagedList<TransactionDto> Transactions { get; set; }
    }
}
