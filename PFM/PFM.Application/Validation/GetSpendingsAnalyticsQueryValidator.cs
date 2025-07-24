using FluentValidation;
using NPOI.SS.Formula.Functions;
using PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics;
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
    public class GetSpendingsAnalyticsQueryValidator : AbstractValidator<GetSpendingsAnalyticsQuery>
    {
        public GetSpendingsAnalyticsQueryValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RuleLevelCascadeMode = CascadeMode.Continue;


            RuleFor(x => x.EndDate)
               .Must((q, end) =>
                   !end.HasValue
                   || !q.StartDate.HasValue
                   || end.Value.Date >= q.StartDate.Value.Date)
               .WithMessage("end-date:combination-required:end-date must be the same or after start-date");

            RuleFor(q => q.Direction)
           .Must(s => Enum.TryParse<DirectionEnum>(s, true, out _))
           .WithMessage(ctx => {
               var names = string.Join(", ", Enum.GetNames(typeof(DirectionEnum)));
               return $"direction:unknown-enum:direction must be one of: {names}";
           }).When(q => !string.IsNullOrWhiteSpace(q.Direction)); ;
        }
    }
}
