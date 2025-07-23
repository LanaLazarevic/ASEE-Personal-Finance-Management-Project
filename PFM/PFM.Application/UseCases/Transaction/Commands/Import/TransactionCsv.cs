using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Commands.Import
{
    public class TransactionCsv
    {
        [Name("id")]
        public string? Id { get; set; }

        [Name("beneficiary-name")]
        public string? BeneficiaryName { get; set; }

        [Name("date")]
        public string? Date { get; set; }

        [Name("direction")]
        public string? Direction { get; set; }

        [Name("amount")]
        public string? Amount { get; set; }

        [Name("description")]
        public string? Description { get; set; }

        [Name("currency")]
        public string? Currency { get; set; }

        [Name("mcc")]
        public string? Mcc { get; set; }

        [Name("kind")]
        public string? Kind { get; set; }
    }
}
