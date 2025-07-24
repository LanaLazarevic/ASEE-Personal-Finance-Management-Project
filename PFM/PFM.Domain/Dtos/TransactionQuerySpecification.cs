using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public enum SortOrder
    {
        Asc,
        Desc
    }

    public class TransactionQuerySpecification
    {
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public IEnumerable<TransactionKind>? Kind { get; }
        public int Page { get; }
        public int PageSize { get; }
        public string SortBy { get; }
        public SortOrder SortOrder { get; }

        public TransactionQuerySpecification(
            DateTime? startDate,
            DateTime? endDate,
            IEnumerable<TransactionKind>? kind,
            int page,
            int pageSize,
            string sortBy,
            SortOrder sortOrder)
        {
            StartDate = startDate;
            EndDate = endDate;
            Kind = kind;
            Page = page <= 0 ? 1 : page;
            PageSize = pageSize <= 0 ? 10 : pageSize;
            SortBy = !string.IsNullOrWhiteSpace(sortBy) ? sortBy : "date";
            SortOrder = sortOrder;
        }
    }
}
