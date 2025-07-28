using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Catagories.Commands.Import
{
    public class CategoryCsv
    {
        [Name("code")]
        public required string Code { get; set; }

        [Name("parent-code")]
        public string? ParentCode { get; set; }

        [Name("name")]
        public required string Name { get; set; }
    }
}
