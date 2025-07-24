using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Result
{
    public class SpendingGroupDto
    {
        [JsonPropertyName("catcode")]
        public string CatCode { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
