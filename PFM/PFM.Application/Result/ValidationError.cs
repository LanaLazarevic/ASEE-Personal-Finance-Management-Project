using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Application.Result
{
    public class ValidationError : Error
    {
        [JsonPropertyName("tag")]
        public required string Tag { get; set; }
        [JsonPropertyName("error")]
        public required string Error { get; set; }
        [JsonPropertyName("message")]
        public required string Message { get; set; }
    }
}
