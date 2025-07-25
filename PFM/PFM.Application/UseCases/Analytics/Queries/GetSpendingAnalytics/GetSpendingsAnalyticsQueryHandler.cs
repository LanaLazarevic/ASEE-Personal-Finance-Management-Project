using FluentValidation;
using MediatR;
using PFM.Application.UseCases.Resault;
using PFM.Application.UseCases.Result;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;
using PFM.Domain.Interfaces;

namespace PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics
{
    public class GetSpendingsAnalyticsQueryHandler : IRequestHandler<GetSpendingsAnalyticsQuery, OperationResult<SpendingsGroupDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetSpendingsAnalyticsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<OperationResult<SpendingsGroupDto>> Handle(GetSpendingsAnalyticsQuery request, CancellationToken cancellationToken)
        {

            DirectionEnum? directionEnum = null;

            if (!string.IsNullOrWhiteSpace(request.Direction))
            {
                directionEnum = Enum.Parse<DirectionEnum>(request.Direction, true);
            }


            var spec = new AnalyticsTransactionQuerySpecification(request.StartDate, request.EndDate, directionEnum);

            var txs = await _uow.Transactions.GetForAnalyticsAsync(spec, cancellationToken);

            var flat = txs.SelectMany(t =>
            (t.Splits != null && t.Splits.Count > 0)
                ? t.Splits.Select(s => new
                        {
                            Cat = s.CatCode ?? string.Empty,
                            Parent = s.Category?.ParentCode,
                            Amount = s.Amount
                        })
                : [ new {
                            Cat = t.CatCode ?? string.Empty,
                            Parent = t.Category?.ParentCode,
                            Amount = t.Amount
                        } ]);


            var groupsQuery = string.IsNullOrWhiteSpace(request.CatCode)
                ? flat.GroupBy(x => string.IsNullOrEmpty(x.Parent) ? x.Cat : x.Parent)
                : flat.Where(x => x.Parent == request.CatCode)
                      .GroupBy(x => x.Cat);

            var groups = groupsQuery
                .Select(g => new SpendingGroupDto
                {
                    CatCode = g.Key,
                    Amount = g.Sum(x => x.Amount),
                    Count = g.Count()
                })
                .ToList();

            return OperationResult<SpendingsGroupDto>.Success(
                new SpendingsGroupDto { Groups = groups }, 200
            );

        }
    }
}
