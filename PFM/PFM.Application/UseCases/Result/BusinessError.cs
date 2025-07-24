using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Result
{
    public class BusinessError : Error
    {
        [JsonPropertyName("problem")]
        public string Problem { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("details")]
        public string Details { get; set; }
       
    }
}
