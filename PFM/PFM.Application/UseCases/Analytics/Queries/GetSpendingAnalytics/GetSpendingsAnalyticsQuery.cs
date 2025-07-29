using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.Result;
using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics
{
    public class GetSpendingsAnalyticsQuery : IRequest<OperationResult<SpendingsGroupDto>>
    {
        [FromQuery(Name = "catcode")]
        public string? CatCode { get; set; }

        [FromQuery(Name = "start-date"), DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [FromQuery(Name = "end-date"), DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [FromQuery(Name = "direction")]
        public string? Direction { get; set; }

    }
}
