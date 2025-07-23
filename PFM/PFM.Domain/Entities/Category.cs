using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Entities
{
    public class Category
    {

        public string Code { get; set; } 

        public string Name { get; set; }

        public string? ParentCode { get; set; }

        public Category? Parent { get; set; }

        public List<Category>? Children { get; set; } = [];

        public List<Transaction>? Transactions { get; set; } = [];

        public List<Split>? Splits { get; set; } = [];

    }
}
