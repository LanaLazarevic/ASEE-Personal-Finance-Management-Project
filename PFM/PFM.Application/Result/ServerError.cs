using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Application.Result
{
    public class ServerError : Error
    {
        [JsonPropertyName("message")]
        public required string Message { get; set; }
    }
}
