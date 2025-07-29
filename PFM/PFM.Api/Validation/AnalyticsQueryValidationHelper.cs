using PFM.Application.Result;
using PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics;
using PFM.Domain.Enums;

namespace PFM.Api.Validation
{
    public static class AnalyticsQueryValidationHelper
    {
        private static readonly HashSet<string> AllowedParams = new(StringComparer.OrdinalIgnoreCase)
        {
            "catcode", "start-date", "end-date", "direction"
        };

        public static (GetSpendingsAnalyticsQuery? Query, List<ValidationError> Errors) ParseAndValidate(IQueryCollection query)
        {
            var errors = new List<ValidationError>();

            foreach (var key in query.Keys)
            {
                if (!AllowedParams.Contains(key))
                {
                    errors.Add(new ValidationError
                    {
                        Tag = key,
                        Error = "not-on-list",
                        Message = $"parameter '{key}' is not on list of valid parameters."
                    });
                }
            }

            string? catCode = query.TryGetValue("catcode", out var catRaw) ? catRaw.ToString() : null;
            if (!string.IsNullOrWhiteSpace(catCode) && int.TryParse(catCode, out _))
            {
                errors.Add(new ValidationError
                {
                    Tag = "catcode",
                    Error = "invalid-type",
                    Message = "catcode must be a string, not a number."
                });
            }

            DateTime? startDate = null;
            if (query.TryGetValue("start-date", out var startRaw))
            {
                if (!DateTime.TryParse(startRaw, out var parsed))
                {
                    errors.Add(new ValidationError
                    {
                        Tag = "start-date",
                        Error = "invalid-format",
                        Message = $"'{startRaw}' is not a valid date."
                    });
                }
                else
                {
                    startDate = parsed.Date;
                }
            }

            DateTime? endDate = null;
            if (query.TryGetValue("end-date", out var endRaw))
            {
                if (!DateTime.TryParse(endRaw, out var parsed))
                {
                    errors.Add(new ValidationError
                    {
                        Tag = "end-date",
                        Error = "invalid-format",
                        Message = $"'{endRaw}' is not a valid date."
                    });
                }
                else
                {
                    endDate = parsed.Date;
                }
            }

            string? direction = query.TryGetValue("direction", out var dirRaw) ? dirRaw.ToString() : null;
            if (!string.IsNullOrWhiteSpace(direction) && !Enum.TryParse<DirectionEnum>(direction, true, out _))
            {
                var names = string.Join(", ", Enum.GetNames(typeof(DirectionEnum)));
                errors.Add(new ValidationError
                {
                    Tag = "direction",
                    Error = "unknown-enum",
                    Message = $"direction must be one of: {names}"
                });
            }

            if (startDate.HasValue && endDate.HasValue && endDate.Value < startDate.Value)
            {
                errors.Add(new ValidationError
                {
                    Tag = "end-date",
                    Error = "combination-required",
                    Message = "end-date must be the same or after start-date"
                });
            }

            if (errors.Any())
                return (null, errors);

            return (new GetSpendingsAnalyticsQuery
            {
                CatCode = catCode,
                StartDate = startDate,
                EndDate = endDate,
                Direction = direction
            }, []);
        }
    }
}
