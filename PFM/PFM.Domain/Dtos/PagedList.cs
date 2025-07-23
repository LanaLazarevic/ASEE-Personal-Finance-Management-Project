using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class PagedList<T>
    {
        [JsonPropertyName("total-count")]
        public int TotalCount { get; set; }
        [JsonPropertyName("page-size")]
        public int PageSize { get; set; }
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("total-pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("sort-orderd")]
        public string SortOrderd { get; set; }
        [JsonPropertyName("sort-by")]
        public string SortBy { get; set; }
        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; }

    }
}
