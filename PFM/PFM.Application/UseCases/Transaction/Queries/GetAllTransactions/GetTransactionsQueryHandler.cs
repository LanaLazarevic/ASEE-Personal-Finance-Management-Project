using MediatR;
using PFM.Application.UseCases.Resault;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Queries.GetAllTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, OperationResult<PagedList<TransactionDto>>>
    {
        private readonly ITransactionRepository _repository;

        public GetTransactionsQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PagedList<TransactionDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            TransactionKind? kindEnum = null;
            if (!string.IsNullOrWhiteSpace(request.Kind))
            {
                kindEnum = Enum.Parse<TransactionKind>(request.Kind, true);
            }
            var spec = new TransactionQuerySpecification(
                request.StartDate,
                request.EndDate,
                kindEnum,
                request.Page,
                request.PageSize,
                request.SortBy,
                request.SortOrder);

            try
            {
                var transactions = await _repository.GetTransactionsAsync(spec);
                return OperationResult<PagedList<TransactionDto>>.Success(transactions);
            }
            catch (TimeoutException tex)
            {
                return OperationResult<PagedList<TransactionDto>>.Fail(
                    "Database timeout, please try again later.");
            }
            catch (Exception)
            {
                return OperationResult<PagedList<TransactionDto>>.Fail(
                    "Unable to fetch transactions.");
            }
        }
    }
}
