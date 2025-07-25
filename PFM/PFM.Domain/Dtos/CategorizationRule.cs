using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Dtos
{
    public class CategorizationRule
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Predicate { get; set; }
    }
}
