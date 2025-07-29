using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Application.Result
{
    public class SpendingsGroupDto
    {
        [JsonPropertyName("groups")]
        public required List<SpendingGroupDto> Groups { get; set; }
    }
}
