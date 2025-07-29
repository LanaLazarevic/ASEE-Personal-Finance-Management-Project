using MediatR;
using PFM.Application.Result;
using PFM.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Commands.SplitTransaction
{
    public record SplitTransactionCommand(
        string TransactionId,
        IEnumerable<SplitItemDto> Splits
    ) : IRequest<OperationResult>;

}
