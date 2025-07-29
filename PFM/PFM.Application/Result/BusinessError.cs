using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Application.Result
{
    public class BusinessError : Error
    {
        [JsonPropertyName("problem")]
        public required string Problem { get; set; }
        [JsonPropertyName("message")]
        public required string Message { get; set; }
        [JsonPropertyName("details")]
        public required string Details { get; set; }
       
    }
}
