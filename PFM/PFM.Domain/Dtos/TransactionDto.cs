using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class TransactionDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("direction")]
        public required string Direction { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("beneficiary-name")]
        public string? BeneficiaryName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;
        [JsonPropertyName("mcc")]
        public MccCodeEnum? Mcc { get; set; }
        [JsonPropertyName("kind")]
        public required string Kind { get; set; }
        [JsonPropertyName("catcode")]
        public string? CatCode { get; set; }

        [JsonPropertyName("splits")]
        public List<SplitItemDto>? Splits { get; set; }
    }

   
}
