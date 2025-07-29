using System.Text.Json.Serialization;
using MediatR;
using NPOI.SS.Formula.Functions;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.Result;

namespace PFM.Application.UseCases.Transaction.Queries.GetAllTransactions
{
    public class GetTransactionsQuery : IRequest<OperationResult<PagedList<TransactionDto>>>
    {
        [FromQuery(Name ="transaction-kind")]
        public IEnumerable<string>? Kind { get; set; }

        [FromQuery(Name = "start-date")]
        public DateTime? StartDate { get; set; }

        [FromQuery(Name = "end-date")]
        public DateTime? EndDate { get; set; }

        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;

        [FromQuery(Name = "page-size")]
        public int PageSize { get; set; } = 10;

        [FromQuery(Name = "sort-by")]
        public string SortBy { get; set; } = "date";

        [FromQuery(Name = "sort-order")]
        public string SortOrder { get; set; } = "Desc";
    }
}
