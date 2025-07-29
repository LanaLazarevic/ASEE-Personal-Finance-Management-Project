using System.Text.Json.Serialization;
using MediatR;
using NPOI.SS.Formula.Functions;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.Result;

namespace PFM.Application.UseCases.Categories.Queries.CetAllCategories
{
    public class GetCatagoriesQuery : IRequest<OperationResult<List<CategoryDto>>>
    {
        [FromQuery(Name = "parent-id")]
        public string? ParentCode { get; set; }
    }
}
