using PFM.Application.Result;
using PFM.Application.UseCases.Transaction.Queries.GetAllTransactions;
using PFM.Domain.Dtos;
using PFM.Domain.Enums;

namespace PFM.Api.Validation
{
    public static class GetAllTransactionQueryValidationHelper
    {
        public static (GetTransactionsQuery? Query, List<ValidationError> Errors) ParseAndValidate(IQueryCollection query)
        {
            var errors = new List<ValidationError>();

            List<string> kinds = [];
            if (query.TryGetValue("transaction-kind", out var kindValues))
            {
                kinds = kindValues
                    .SelectMany(k => k?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [])
                    .ToList();

                var enumNames = Enum.GetNames(typeof(TransactionKind));
                var validNames = new HashSet<string>(enumNames, StringComparer.OrdinalIgnoreCase);

                foreach (var kind in kinds)
                {
                    if (int.TryParse(kind, out _)
                        || !validNames.Contains(kind))
                    {
                        errors.Add(new ValidationError
                        {
                            Tag = "transaction-kind",
                            Error = "unknown-enum",
                            Message = $"transaction-kind must be one of: {string.Join(", ", enumNames)}"
                        });
                        break;
                    }
                }
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
                    startDate = parsed;
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
                    endDate = parsed;
                }
            }

            int page = 1;
            if (query.TryGetValue("page", out var pageRaw))
            {
                if (!int.TryParse(pageRaw, out page))
                {
                    errors.Add(new ValidationError
                    {
                        Tag = "page",
                        Error = "invalid-format",
                        Message = $"'{pageRaw}' is not a valid number."
                    });
                }
                else if (page < 1)
                {
                    errors.Add(new ValidationError
                    {
                        Tag = "page",
                        Error = "out-of-range",
                        Message = "Page must be at least 1."
                    });
                }
            }


            int pageSize = 10;
            if (query.TryGetValue("page-size", out var sizeRaw))
            {
                if (!int.TryParse(sizeRaw, out pageSize))
                {
                    errors.Add(new ValidationError
                    {
                        Tag = "page-size",
                        Error = "invalid-format",
                        Message = $"'{sizeRaw}' is not a valid number."
                    });
                }
                else if (pageSize < 1 || pageSize > 100)
                {
                    errors.Add(new ValidationError
                    {
                        Tag = "page-size",
                        Error = "out-of-range",
                        Message = "page-size must be between 1 and 100."
                    });
                }
            }

            string sortOrder = query.TryGetValue("sort-order", out var orderRaw) ? orderRaw.ToString() : "Desc";
            if (!Enum.TryParse<SortOrder>(sortOrder, true, out _))
            {
                var names = string.Join(", ", Enum.GetNames(typeof(SortOrder)));
                errors.Add(new ValidationError
                {
                    Tag = "sort-order",
                    Error = "unknown-enum",
                    Message = $"sort-order must be one of: {names}"
                });
            }

            string sortBy = query.TryGetValue("sort-by", out var sortRaw) ? sortRaw.ToString() : "date";
            if (!string.IsNullOrWhiteSpace(sortBy) && int.TryParse(sortBy, out _))
            {
                errors.Add(new ValidationError
                {
                    Tag = "sort-by",
                    Error = "invalid-type",
                    Message = "sort-by must be a string, not a number."
                });
            }


            if (startDate.HasValue && endDate.HasValue && endDate.Value.Date < startDate.Value.Date)
            {
                errors.Add(new ValidationError
                {
                    Tag = "end-date",
                    Error = "combination-required",
                    Message = "end-date must be the same or after start-date."
                });
            }

            var allowedParams = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "transaction-kind",
                "start-date",
                "end-date",
                "page",
                "page-size",
                "sort-by",
                "sort-order"
            };

            foreach (var key in query.Keys)
            {
                if (!allowedParams.Contains(key))
                {
                    errors.Add(new ValidationError
                    {
                        Tag = key,
                        Error = "not-on-list",
                        Message = $"parameter '{key}' is not on list of valid parameters."
                    });
                }
            }



            if (errors.Any())
                return (null, errors);

            var model = new GetTransactionsQuery
            {
                Kind = kinds,
                StartDate = startDate,
                EndDate = endDate,
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder
            };


            return (model, []);
        }
    }
}
