using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PFM.Api.Request
{
    public class CategorizeTransactionRequest
    {
        [JsonPropertyName("catcode")]
        public required string  CategoryCode { get; set; }
    }
}
