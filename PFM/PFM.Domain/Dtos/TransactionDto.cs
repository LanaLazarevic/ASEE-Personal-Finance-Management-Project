using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class TransactionDto
    {
        public string Id { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Direction { get; set; }
        public double Amount { get; set; }
        public string? BeneficiaryName { get; set; }
        public string? Description { get; set; }
        public string Currency { get; set; } = default!;

        public MccCodeEnum? Mcc { get; set; }
        public string Kind { get; set; }

        public string? CatCode { get; set; }
    }

   
}
