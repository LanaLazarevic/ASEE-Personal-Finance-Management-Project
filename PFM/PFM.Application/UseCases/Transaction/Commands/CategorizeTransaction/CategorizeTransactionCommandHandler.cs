using MediatR;
using PFM.Application.Result;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Commands.CategorizeTransaction
{
    public class CategorizeTransactionCommandHandler : IRequestHandler<CategorizeTransactionCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;

        public CategorizeTransactionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<OperationResult> Handle(CategorizeTransactionCommand request, CancellationToken cancellationToken)
        {
            var tx = await _uow.Transactions.GetByIdAsync(request.TransactionId, cancellationToken);

            if (tx == null)
            {
                BusinessError error = new BusinessError
                {
                    Problem = "provided-transaction-does-not-exists",
                    Details = $"Transaction '{request.TransactionId}' not found.",
                    Message = "The provided transaction does not exist."
                };
                List<BusinessError> errors = new List<BusinessError> { error };
                return OperationResult.Fail(440, errors);

            }


            var cats = await _uow.Categories.GetByCodesAsync(new[] { request.CategoryCode }, cancellationToken);
            var cat = cats.SingleOrDefault();
            if (cat == null)
            {
                BusinessError error = new BusinessError
                {
                    Problem = "provided-category-does-not-exists",
                    Details = $"Category '{request.CategoryCode}' not found.",
                    Message = "The provided category does not exist."
                };
                List<BusinessError> errors = new List<BusinessError> { error };
                return OperationResult.Fail(440, errors);

            }

            tx.CatCode = cat.Code;

            await _uow.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
    }
}
