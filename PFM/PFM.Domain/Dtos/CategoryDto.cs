using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class CategoryDto
    {
        [JsonPropertyName("parent-code")]
        public required string ParentCode  { get; set; }
        [JsonPropertyName("code")]
        public required string Code { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }
}
