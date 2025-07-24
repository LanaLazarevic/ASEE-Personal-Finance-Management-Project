using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics;
using PFM.Application.UseCases.Result;
using Swashbuckle.AspNetCore.Annotations;

namespace PFM.Api.Controllers
{
    [Route("spending-analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnalyticsController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(
            OperationId = "Spendings_Get",
            Summary = "Retrieve spending analytics by category or by subcategories within category",
            Description = "Retrieves spending analytics by category or by subcategories within category"
        )]
        public async Task<IActionResult> Get(
            [FromQuery] GetSpendingsAnalyticsQuery query)
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
                    return StatusCode(op.code, errors);
                }


                return StatusCode(op.code, new { errors });

            }

            return Ok(op.Value);
        }
    }
}
