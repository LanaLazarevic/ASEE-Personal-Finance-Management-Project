using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Commands.Import
{
    public record ImportTransactionsCommand(IEnumerable<TransactionCsv> Transactions) : IRequest<bool>
    {
    }
}
