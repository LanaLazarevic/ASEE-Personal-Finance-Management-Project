using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.Result;
using PFM.Application.UseCases.Catagories.Commands.Import;
using PFM.Application.UseCases.Categories.Queries.CetAllCategories;
using PFM.Application.UseCases.Transaction.Commands.AutoCategorization;
using PFM.Application.UseCases.Transaction.Commands.Import;
using PFM.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PFM.Api.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
            => _mediator = mediator;

        [SwaggerOperation(OperationId = "Category_Import",
                  Summary = "Import categories",
                  Description = "Imports categories via CSV")]
        [HttpPost("import")]
        [Consumes("application/csv")]
        public async Task<IActionResult> Import([FromBody] ImportCategoriesCommand cmd)
        {

            if (!ModelState.IsValid)
            {
                
                    var errors = ModelState
                       .SelectMany(kvp => kvp.Value?.Errors
                       .Select(err =>
                       {
                           var raw = err.ErrorMessage ?? "";
                           var idx = raw.IndexOf(':');
                           var code = idx > 0 ? raw.Substring(0, idx) : "invalid-format";
                           var message = idx > 0 ? raw.Substring(idx + 1) : raw;
                           var tag = kvp.Key;
                           if (kvp.Key == "cmd")
                           {
                               tag = "file";
                               message = "Invalid file format so the command coudnt be processed";
                           } else if(kvp.Key == "")
                           {
                               tag = "body";
                           }
                           return new ValidationError
                               {
                                   Tag = tag,
                                   Error = code,
                                   Message = message
                           };
                       }) ?? [])
                       .ToList();

                    return BadRequest(new {errors});

            }

            var op = await _mediator.Send(cmd);
            if (!op.IsSuccess)
            {
                object? errors = null;
                if (op.code == 400)
                {
                    errors = op.Error!
                    .OfType<ValidationError>()
                    .Select(e => new
                    {
                        tag = e.Tag,
                        error = e.Error,
                        message = e.Message
                    })
                    .ToList();
                    return StatusCode(op.code, new { errors });
                }
                else if (op.code == 503)
                {
                    errors = op.Error!
                    .OfType<ServerError>()
                    .Select(e => new
                    {
                        message = e.Message
                    })
                    .ToList();
                    return StatusCode(op.code, errors);
                }


                return StatusCode(op.code, new { errors });
            }

            return Ok("Categories imported successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetCatagoriesQuery cmd)
        {

            if (!ModelState.IsValid)
            {

                var errors = ModelState
                   .SelectMany(kvp => kvp.Value?.Errors
                   .Select(err =>
                   {
                       var raw = err.ErrorMessage ?? "";
                       var idx = raw.IndexOf(':');
                       var code = idx > 0 ? raw.Substring(0, idx) : "invalid-format";
                       var message = idx > 0 ? raw.Substring(idx + 1) : raw;
                       var tag = kvp.Key;
                       if (kvp.Key == "cmd")
                       {
                           tag = "catcode";
                           message = "Invalid query format so the command coudn't be processed";
                       }
                       else if (kvp.Key == "")
                       {
                           tag = "body";
                       }
                       return new ValidationError
                       {
                           Tag = tag,
                           Error = code,
                           Message = message
                       };
                   }) ?? [])
                   .ToList();

                return BadRequest(new { errors });

            }

            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
            {
                var errors = result.Error!
                    .OfType<ServerError>()
                    .Select(e => new
                    {
                        message = e.Message
                    })
                    .ToList();
                return StatusCode(result.code, errors);
            }

            return Ok(new { items = result.Value});
        }
    }
}
