using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.UseCases.Catagories.Commands.Import;
using PFM.Application.UseCases.Result;
using PFM.Application.UseCases.Transaction.Commands.Import;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                       .SelectMany(kvp => kvp.Value.Errors
                       .Select(err =>
                       {
                           var raw = err.ErrorMessage ?? "";
                           var idx = raw.IndexOf(':');
                           var code = idx > 0 ? raw.Substring(0, idx) : "invalid-format";
                           var message = idx > 0 ? raw.Substring(idx + 1) : raw;
                           return new ValidationError
                           {
                               Tag = kvp.Key,
                               Error = code,
                               Message = message
                           };
                       }))
                       .ToList();

                    return BadRequest(errors);
                }

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
                }


                return StatusCode(op.code, new { errors });
            }

            return Ok("Categories imported successfully");
        }
    }
}
