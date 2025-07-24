using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class AnalyticsTransactionQuerySpecification
    {
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public DirectionEnum? Direction { get; }

        public AnalyticsTransactionQuerySpecification(
            DateTime? startDate,
            DateTime? endDate,
            DirectionEnum? direction)
        {
            StartDate = startDate;
            EndDate = endDate;
            Direction = direction;
        }
    }
}
