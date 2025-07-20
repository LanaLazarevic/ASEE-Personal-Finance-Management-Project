using MediatR;
using PFM.Application.UseCases.Resault;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Queries.GetAllTransactions
{
    public class GetTransactionsQuery : IRequest<OperationResult<PagedList<TransactionDto>>>,
      IValidatableObject
    {
        public string? Kind { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page must be at least 1.")]
        public int Page { get; set; } = 1;
        [Range(1, 1000, ErrorMessage = "PageSize must be between 1 and 1000.")]
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "date";
        public SortOrder SortOrder { get; set; } = SortOrder.Desc;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Kind) &&
                !Enum.TryParse<TransactionKind>(Kind, true, out _))
            {
                var names = string.Join(", ", Enum.GetNames(typeof(TransactionKind)));
                yield return new ValidationResult(
                    $"Kind must be one of: {names}",
                    new[] { nameof(Kind) }
                );
            }

            if (StartDate.HasValue && EndDate.HasValue &&
                EndDate.Value.Date < StartDate.Value.Date)
            {
                yield return new ValidationResult(
                    "EndDate must be the same or after StartDate",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}
