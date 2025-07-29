using PFM.Application.Result;
using PFM.Domain.Dtos;

namespace PFM.Api.Validation
{
    public static class SplitTransactionValidatorHelper
    {
        public static List<ValidationError> Validate(List<SplitItemDto>? splits)
        {
            var errors = new List<ValidationError>();

            if (splits == null || splits.Count()<2)
            {
                errors.Add(new ValidationError
                {
                    Tag = "splits",
                    Error = "required",
                    Message = "At least two split items are required."
                });
                return errors;
            }

            for (int i = 0; i < splits.Count; i++)
            {
                var split = splits[i];
                var prefix = $"splits[{i}]";

                if (string.IsNullOrWhiteSpace(split.CatCode))
                {
                    errors.Add(new ValidationError
                    {
                        Tag = $"{prefix}.catcode",
                        Error = "required",
                        Message = "catcode is required and must be a non-empty string."
                    });
                }

                if (split.Amount <= 0)
                {
                    errors.Add(new ValidationError
                    {
                        Tag = $"{prefix}.amount",
                        Error = "out-of-range",
                        Message = "amount must be greater than 0."
                    });
                }
            }

            return errors;
        }
    }
}
