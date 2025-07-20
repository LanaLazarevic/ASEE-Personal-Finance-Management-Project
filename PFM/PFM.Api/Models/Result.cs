using Microsoft.AspNetCore.Mvc;

namespace PFM.Api.Models
{
    public class Result
    {
        public string? Message { get; init; }
        public ActionResult ActionResult { get; init; }

        private Result(ActionResult actionResult, string? message = null)
        {
            ActionResult = actionResult;
            Message = message;
        }

        public static Result Ok(string? message = null)
            => new(new OkResult(), message);

        public static Result NoContent()
            => new(new NoContentResult());

        public static Result BadRequest(string? message = null)
            => new(new BadRequestObjectResult(message), message);

        public static Result UnprocessableEntity(string? message = null)
            => new(new ObjectResult(message) { StatusCode = 422 }, message);

        public static Result ServiceUnavailable(string? message = null)
            => new(new ObjectResult(message) { StatusCode = 503 }, message);
    }

    public class Result<T>
    {
        public T? Value { get; init; }
        public string? Message { get; init; }
        public ActionResult ActionResult { get; init; }

        private Result(ActionResult actionResult, T? value, string? message = null)
        {
            ActionResult = actionResult;
            Value = value;
            Message = message;
        }

        public static Result<T> Ok(T value, string? message = null)
            => new(new OkObjectResult(value), value, message);

        public static Result<T> NoContent()
            => new(new NoContentResult(), default, null);

        public static Result<T> BadRequest(string? message = null)
            => new(new BadRequestObjectResult(message), default, message);

        public static Result<T> UnprocessableEntity(string? message = null)
            => new(new ObjectResult(message) { StatusCode = 422 }, default, message);

        public static Result<T> ServiceUnavailable(string? message = null)
            => new(new ObjectResult(message) { StatusCode = 503 }, default, message);
    }
}
