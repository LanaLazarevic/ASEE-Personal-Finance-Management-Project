using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Entities
{
    public class Transaction
    {
        public string Id { get; set; }

        public string? BeneficiaryName { get; set; }

        public DateTime Date { get; set; }

        public DirectionEnum Direction { get; set; }

        public double Amount { get; set; }

        public string? Description { get; set; }

        public string Currency { get; set; }

        public MccCodeEnum? Mcc { get; set; }

        public TransactionKind Kind { get; set; }

        public string? CatCode { get; set; }

        public Category? Category { get; set; }

        public List<Split>? Splits { get; set; } = [];
    }
}
