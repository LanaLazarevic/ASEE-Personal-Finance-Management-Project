using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using PFM.Application.UseCases.Catagories.Commands.Import;
using System.Globalization;
using System.Text;

namespace PFM.Api.Formatters
{
    public class CsvInputFormatter : TextInputFormatter
    {
        public CsvInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(ImportCategoriesCommand)
                || type == typeof(IEnumerable<CategoryCsv>)
                || type == typeof(List<CategoryCsv>);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context, Encoding encoding)
        {
            string csvContent;
            using (var reader = new StreamReader(context.HttpContext.Request.Body, encoding))
            {
                csvContent = await reader.ReadToEndAsync();
            }

            using var stringReader = new StringReader(csvContent);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                BadDataFound = null,
                MissingFieldFound = null
            };

            List<CategoryCsv> records;
            try
            {
                using var csv = new CsvReader(stringReader, config);
                records = csv.GetRecords<CategoryCsv>().ToList();
            }
            catch (Exception ex)
            {
                context.ModelState.TryAddModelError(
                    context.ModelName,
                    "Invalid CSV format: " + ex.Message);
                return await InputFormatterResult.FailureAsync();
            }

            if (context.ModelType == typeof(ImportCategoriesCommand))
            {
                var cmd = new ImportCategoriesCommand(records);
                return await InputFormatterResult.SuccessAsync(cmd);
            }

            return await InputFormatterResult.SuccessAsync(records);
        }
    }
}

