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

            if(!string.IsNullOrWhiteSpace(request.CatCode))
            {
                var cats = await _uow.Categories.GetByCodesAsync(new[] { request.CatCode }, cancellationToken);
                var cat = cats.SingleOrDefault();
                if (cat == null)
                {
                    BusinessError error = new BusinessError
                    {
                        Problem = "provided-category-does-not-exists",
                        Details = $"Category '{request.CatCode}' not found.",
                        Message = "The provided category does not exist."
                    };
                    List<BusinessError> errors = new List<BusinessError> { error };
                    return OperationResult<SpendingsGroupDto>.Fail(440, errors);

                }
            }
           
            DirectionEnum? directionEnum = null;

            if (!string.IsNullOrWhiteSpace(request.Direction))
            {
                directionEnum = Enum.Parse<DirectionEnum>(request.Direction, true);
            }

            DateTime? startUtc = request.StartDate.HasValue
                        ? DateTime.SpecifyKind(request.StartDate.Value, DateTimeKind.Utc)
                        : (DateTime?)null;
            DateTime? endUtc = request.EndDate.HasValue
                        ? DateTime.SpecifyKind(request.EndDate.Value, DateTimeKind.Utc)
                        : (DateTime?)null;

            var spec = new AnalyticsTransactionQuerySpecification(startUtc, endUtc, directionEnum);

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
