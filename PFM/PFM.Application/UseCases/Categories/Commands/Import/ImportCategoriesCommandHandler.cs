
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using PFM.Domain.Entities;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Catagories.Commands.Import
{
    public class ImportCategoriesCommandHandler : IRequestHandler<ImportCategoriesCommand, bool>
    {
        private readonly ICategoryRepository _repo;
        private readonly IUnitOfWork _uow;

        public ImportCategoriesCommandHandler(
            ICategoryRepository repo,
            IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<bool> Handle(ImportCategoriesCommand request, CancellationToken ct)
        {
            var records = request.Records;

            var valid = records
                .Where(r => !string.IsNullOrWhiteSpace(r.Code)
                         && !string.IsNullOrWhiteSpace(r.Name))
                .ToList();

            if (!valid.Any())
                return false;


            var codes = valid.Select(r => r.Code).Distinct();
            var parentCodes = valid
                .Select(r => r.ParentCode)
                .Where(pc => !string.IsNullOrWhiteSpace(pc))
                .Distinct();

            var existing = await _repo.GetByCodesAsync(codes.Concat(parentCodes), ct);
            var dict = existing
                .Where(c => codes.Contains(c.Code))
                .ToDictionary(c => c.Code);

            foreach (var row in valid)
            {
                if (!dict.ContainsKey(row.Code))
                {
                    var cat = new Category
                    {
                        Code = row.Code,
                        Name = row.Name
                    };
                    _repo.Add(cat);
                    dict.Add(cat.Code, cat);
                }
            }

            foreach (var row in valid)
            {
                var cat = dict[row.Code];
                cat.Name = row.Name;

                var pc = string.IsNullOrWhiteSpace(row.ParentCode)
                    ? null
                    : row.ParentCode;

                if (!string.IsNullOrEmpty(pc) && !dict.ContainsKey(pc))
                {
                    var p = existing.FirstOrDefault(x => x.Code == pc);
                    if (p != null) dict[pc] = p;
                    else pc = null;
                }

                cat.ParentCode = pc;
            }


            await _uow.SaveChangesAsync(ct);
            return true;
        }

       
        private List<CategoryCsv> ParseCsv(string csvContent)
        {
            if (string.IsNullOrWhiteSpace(csvContent))
                throw new InvalidDataException("CSV content is empty.");

            var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null
            };

            try
            {
                using var reader = new StringReader(csvContent);
                using var csv = new CsvReader(reader, config);
                return csv.GetRecords<CategoryCsv>().ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Invalid CSV format.", ex);
            }
        }
    }
}
