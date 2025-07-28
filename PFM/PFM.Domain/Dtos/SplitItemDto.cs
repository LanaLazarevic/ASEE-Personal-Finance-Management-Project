using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class SplitItemDto
    {
        [JsonPropertyName("catcode")]
        public required string CatCode { get; set; }
        [JsonPropertyName("amount")]
        public required double Amount { get; set; }
    }
}
