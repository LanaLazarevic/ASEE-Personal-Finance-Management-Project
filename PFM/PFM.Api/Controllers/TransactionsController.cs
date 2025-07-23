using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.UseCases.Result;
using PFM.Application.UseCases.Transaction.Commands.Import;
using PFM.Application.UseCases.Transaction.Queries.GetAllTransactions;
using PFM.Domain.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace PFM.Api.Controllers
{
    [Route("transactions")]
    [ApiController]
    public class TransactionsController : Controller
    {

        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(OperationId = "Transactions_Import", Summary = "Import transactions", Description = "Imports transactions via CSV")]
        [HttpPost("import")]
        [Consumes("application/csv")]
        public async Task<IActionResult> Import([FromBody] ImportTransactionsCommand command)
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
            var op = await _mediator.Send(command);
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

            return Ok("Transactions imported successfully");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTransactionsQuery query)
        {
            //invalid format uhvatiti u validatoru
            if (!ModelState.IsValid)
            {
               
                   var errors = ModelState
                        .SelectMany(kvp => kvp.Value.Errors
                        .Select(err =>
                        {
                            var raw = err.ErrorMessage ?? "";
                            var idx = raw.IndexOf(':');
                            var code = idx > 0 ? raw[..idx] : "invalid-format";
                            var message = idx > 0 ? raw[(idx + 1)..] : raw;
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
            var op = await _mediator.Send(query);

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
                    return StatusCode(op.code, errors );
                }


                return StatusCode(op.code, new { errors });
            }
                

            return Ok(op.Value);
        }
    }
}
