using FluentValidation;
using MediatR;
using NPOI.SS.Formula.Functions;
using PFM.Application.UseCases.Resault;
using PFM.Application.UseCases.Result;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Queries.GetAllTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, OperationResult<PagedList<TransactionDto>>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<GetTransactionsQuery> _validator;

        public GetTransactionsQueryHandler(IUnitOfWork repository, IValidator<GetTransactionsQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<OperationResult<PagedList<TransactionDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors;
                List<ValidationError> validationerrors = new List<ValidationError>();
                foreach (var error in result.Errors)
                {
                    var raw = error.ErrorMessage ?? "";

                    var split = raw.Split([':'], 3);

                    var tag = split[0].Trim();
                    var code = split[1].Trim();
                    var message = split[2].Trim();
                    var newError = new ValidationError
                    {
                        Tag = tag,
                        Error = code,
                        Message = message
                    };
                    validationerrors.Add(newError);
                }
               
                return OperationResult<PagedList<TransactionDto>>.Fail(400, validationerrors);
            }
            TransactionKind? kindEnum = null;
            if (!string.IsNullOrWhiteSpace(request.Kind))
                kindEnum = Enum.Parse<TransactionKind>(request.Kind, true);

            var sortEnum = Enum.Parse<SortOrder>(request.SortOrder, true);

            var spec = new TransactionQuerySpecification(
                request.StartDate,
                request.EndDate,
                kindEnum,
                request.Page,
                request.PageSize,
                request.SortBy,
                sortEnum
            );

            try
            {
                var transactions = await _repository.Transactions.GetTransactionsAsync(spec);
                return OperationResult<PagedList<TransactionDto>>.Success(transactions, 200);
            }
            catch (TimeoutException tex)
            {
                var error = "An error occurred while fetching transactions. The request timed out." + tex.Message;
                var problem = new ServerError()
                {
                    Message = error
                };
                List<ServerError> problems = new List<ServerError> { problem };
                return OperationResult<PagedList<TransactionDto>>.Fail(503,  problems); 
            }
            catch (Exception)
            {
                var error = "An error occurred while fetching transactions.";
                var problem = new ServerError()
                {
                    Message = error
                };
                List<ServerError> problems = new List<ServerError> { problem };
                return OperationResult<PagedList<TransactionDto>>.Fail(503, problems);
            }
        }
    }
}
