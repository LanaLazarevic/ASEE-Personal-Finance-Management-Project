using MediatR;
using PFM.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Commands.CategorizeTransaction
{
    public record CategorizeTransactionCommand(string TransactionId, string CategoryCode)
         : IRequest<OperationResult>;
}
