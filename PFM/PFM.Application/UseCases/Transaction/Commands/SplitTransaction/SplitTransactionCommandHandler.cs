using MediatR;
using PFM.Application.Result;
using PFM.Domain.Dtos;
using PFM.Domain.Entities;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Commands.SplitTransaction
{
    public class SplitTransactionCommandHandler : IRequestHandler<SplitTransactionCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;
        
        public SplitTransactionCommandHandler(IUnitOfWork ouw)
        {
            _uow = ouw;
        }

        public async Task<OperationResult> Handle(SplitTransactionCommand request, CancellationToken cancellationToken)
        {
            var tx = await _uow.Transactions.GetForAnalyticsAsync(
                         new AnalyticsTransactionQuerySpecification(
                             null, null, null), cancellationToken)
                      .ContinueWith(t => t.Result
                         .FirstOrDefault(x => x.Id == request.TransactionId),
                         cancellationToken);
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

            
            
            var codes = request.Splits.Select(s => s.CatCode).Distinct();
            var cats = await _uow.Categories.GetByCodesAsync(codes, cancellationToken);
            if (cats.Count != codes.Count())
            {
                BusinessError error = new BusinessError
                {
                    Problem = "provided-category-does-not-exists",
                    Details = "Categories provided not found.",
                    Message = "The provided categories do not exist."
                };
                List<BusinessError> errors = new List<BusinessError> { error };
                Console.WriteLine();
                return OperationResult.Fail(440, errors);
            }

            var sum = request.Splits.Sum(s => s.Amount);
            if (Math.Abs(sum - tx.Amount) != 0)
            {
                BusinessError error = new BusinessError
                {
                    Problem = "split-amount-over-transaction-amount",
                    Details = "The sum of splits amount does not match the transaction amount.",
                    Message = "The split amount is over/under transaction amount "
                };
                List<BusinessError> errors = new List<BusinessError> { error };
                Console.WriteLine();
                return OperationResult.Fail(440, errors);
            }

            if (tx.Splits != null)
            {
                tx.Splits.Clear();
            }else
            {
                tx.Splits = new List<Split>();
            }

            foreach (var s in request.Splits)
            {
                var cat = cats.First(c => c.Code == s.CatCode);
               
                tx.Splits.Add(new Split
                {
                    TransactionId = tx.Id,
                    CatCode = s.CatCode,
                    Amount = s.Amount,
                    Category = cat,
                    Transaction = tx
                });
                
            }

            await _uow.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
    }
}
