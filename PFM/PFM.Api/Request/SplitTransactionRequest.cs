using PFM.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PFM.Api.Request
{
    public class SplitTransactionRequest
    {
        [JsonPropertyName("splits")]
        [Required]
        public IEnumerable<SplitItemDto> Splits { get; set; }
    }
}
