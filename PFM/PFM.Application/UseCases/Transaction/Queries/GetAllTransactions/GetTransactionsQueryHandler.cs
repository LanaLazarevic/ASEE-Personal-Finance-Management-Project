using AutoMapper;
using FluentValidation;
using MediatR;
using NPOI.SS.Formula.Functions;
using PFM.Application.Result;
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
    public class GetCategoriesQueryHandler : IRequestHandler<GetTransactionsQuery, OperationResult<PagedList<TransactionDto>>>
    {
        private readonly IUnitOfWork _repository;

        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedList<TransactionDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            
            List<TransactionKind>? kindsEnum = null;
            if (request.Kind != null && request.Kind.Any())
            {
                var parsed = new List<TransactionKind>();
                foreach (var kindParam in request.Kind)
                {
                    var parts = kindParam
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (var part in parts)
                    {
                        if (Enum.TryParse<TransactionKind>(part, true, out var val))
                        {
                            parsed.Add(val);
                        }
                    }
                }
                if (parsed.Any())
                    kindsEnum = parsed.Distinct().ToList();
            }

            var sortEnum = Enum.Parse<SortOrder>(request.SortOrder, true);

            DateTime? startUtc = request.StartDate.HasValue
                        ? DateTime.SpecifyKind(request.StartDate.Value, DateTimeKind.Utc)
                        : (DateTime?)null;
            DateTime? endUtc = request.EndDate.HasValue
                        ? DateTime.SpecifyKind(request.EndDate.Value, DateTimeKind.Utc)
                        : (DateTime?)null;

            var spec = new TransactionQuerySpecification(
                startUtc,
                endUtc,
                kindsEnum,
                request.Page,
                request.PageSize,
                request.SortBy,
                sortEnum
            );

            try
            {
                var transactions = await _repository.Transactions.GetTransactionsAsync(spec);

                if (request.Page > transactions.TotalPages && transactions.TotalPages != 0)
                {
                    ValidationError error = new ValidationError
                    {
                        Tag = "page",
                        Error = "out-of-range",
                        Message = $"Page {request.Page} is out of range. Total pages: {transactions.TotalPages}."
                    };
                    List<ValidationError> errors = new List<ValidationError> { error };
                    return OperationResult<PagedList<TransactionDto>>.Fail(440, errors);
                }
                var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions.Items);
                var pagedList = new PagedList<TransactionDto>()
                {
                    Items = transactionDtos,
                    TotalCount = transactions.TotalCount,
                    PageSize = transactions.PageSize,
                    Page = transactions.Page,
                    SortBy = transactions.SortBy,
                    SortOrderd = transactions.SortOrderd,
                    TotalPages = transactions.TotalPages
                };
                return OperationResult<PagedList<TransactionDto>>.Success(pagedList, 200);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
