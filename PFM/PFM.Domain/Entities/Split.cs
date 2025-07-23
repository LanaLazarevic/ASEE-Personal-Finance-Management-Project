using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Entities
{
    public class Split
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string CatCode { get; set; }
        public double Amount { get; set; }
        public Category Category { get; set; }
        public Transaction Transaction { get; set; }

    }
}
