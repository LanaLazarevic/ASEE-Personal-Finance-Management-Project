using FluentValidation;
using PFM.Application.UseCases.Transaction.Queries.GetAllTransactions;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.Validation
{
    public class GetTransactionsQueryValidator : AbstractValidator<GetTransactionsQuery>
    {
        public GetTransactionsQueryValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RuleLevelCascadeMode = CascadeMode.Continue;


            RuleFor(x => x.Kind)
             .Must(list => list == null || list.All(k => Enum.TryParse<TransactionKind>(k.Trim(), true, out _)))
             .WithMessage(ctx => {
                 var names = string.Join(", ", Enum.GetNames(typeof(TransactionKind)));
                 return $"transaction-kind:unknown-enum:transaction-kind must be one of: {names}";
             });

            RuleFor(x => x.EndDate)
                .Must((q, end) =>
                    !end.HasValue
                    || !q.StartDate.HasValue
                    || end.Value.Date >= q.StartDate.Value.Date)
                .WithMessage("end-date:combination-required:end-date must be the same or after start-date");

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("page:out-of-range:page must be at least 1");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 1000)
                .WithMessage("page-size:out-of-range:page-size must be between 1 and 1000");

            RuleFor(q => q.SortOrder)
            .Must(s => Enum.TryParse<SortOrder>(s, true, out _))
            .WithMessage(ctx => {
                var names = string.Join(", ", Enum.GetNames(typeof(SortOrder)));
                return $"sort-order:unknown-enum:sort-order must be one of: {names}";
            });
        }
    }
}
