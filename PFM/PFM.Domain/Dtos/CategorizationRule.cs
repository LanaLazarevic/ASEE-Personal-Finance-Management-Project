using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class CategorizationRule
    {
        public required string Code { get; set; }
        public required string Title { get; set; }
        public required string Predicate { get; set; }
    }
}
