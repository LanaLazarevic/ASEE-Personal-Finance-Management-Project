using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PFM.Application.Result;
using PFM.Domain.Dtos;
using PFM.Domain.Entities;
using PFM.Domain.Enums;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Transaction.Commands.Import
{
    public class ImportTransactionsCommandHandler : IRequestHandler<ImportTransactionsCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITransactionImportLogger _logger;
        public ImportTransactionsCommandHandler(IUnitOfWork uow, ITransactionImportLogger logger)
        {
            _uow = uow;
            _logger = logger;
        }


        public async Task<OperationResult> Handle(ImportTransactionsCommand request, CancellationToken cancellationToken)
        {
            var valid = new List<TransactionCsv>();

            foreach (var r in request.Transactions)
            {
                if (string.IsNullOrWhiteSpace(r.Id)
                    || !DateTime.TryParseExact(r.Date?.Trim(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                    || string.IsNullOrWhiteSpace(r.Direction)
                    || !double.TryParse(r.Amount?.Replace("€", string.Empty).Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out _)
                    || string.IsNullOrWhiteSpace(r.Currency)
                    || string.IsNullOrWhiteSpace(r.Kind))
                    continue;
                valid.Add(r);
            }

            if (!valid.Any())
            {
                return OperationResult.Fail(
                    400,
                    new List<ValidationError>
                    {
                        new ValidationError
                        {
                            Tag = "file",
                            Message = "File doesnt contain a signle valid row.",
                            Error = "invalid-format"
                        }
                    });
            }

            var allIds = valid.Select(r => r.Id).Distinct().ToList();
            var existingIds = await _uow.Transactions.GetExistingIdsAsync(allIds, cancellationToken);

            var skippedIds = new List<string>();

            try
            {
                foreach (var row in valid)
                {
                    if(row == null || string.IsNullOrWhiteSpace(row.Id))
                        continue;

                    if (existingIds.Contains(row.Id))
                    {
                        skippedIds.Add(row.Id);
                        continue;
                    }

                    var parsed = DateTime.ParseExact(row.Date.Trim(), "M/d/yyyy", CultureInfo.InvariantCulture);
                    var date = DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
                    var amount = double.Parse(row.Amount.Replace("€", string.Empty).Trim(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var direction = (DirectionEnum)Enum.Parse(typeof(DirectionEnum), row.Direction, true);
                    var currency = row.Currency.Trim();
                    var mcc = string.IsNullOrWhiteSpace(row.Mcc)
                              ? (MccCodeEnum?)null
                              : (MccCodeEnum)Enum.Parse(typeof(MccCodeEnum), row.Mcc);
                    var kind = (TransactionKind)Enum.Parse(typeof(TransactionKind), row.Kind, true);

                    var tx = new PFM.Domain.Entities.Transaction
                    {
                        Id = row.Id,
                        BeneficiaryName = row.BeneficiaryName,
                        Date = date,
                        Direction = direction,
                        Amount = amount,
                        Description = row.Description,
                        Currency = currency,
                        Mcc = mcc,
                        Kind = kind
                    };

                    _uow.Transactions.Add(tx);
                }

                await _uow.SaveChangesAsync(cancellationToken);
                await _logger.LogSkippedAsync(skippedIds, cancellationToken);
                return OperationResult.Success();

            }
            catch (DbUpdateException dbEx)
            {
                var error = "Database error while importing transactions: " + dbEx.Message;
                var problem = new ServerError()
                {
                    Message = error
                };
                List<ServerError> problems = new List<ServerError> { problem };
                return OperationResult.Fail(503, problems);
            }
            catch (NpgsqlException npgEx)
            {
                var error = "PostgreSQL error while importing transactions: " + npgEx.Message;
                var problem = new ServerError()
                {
                    Message = error
                };
                List<ServerError> problems = new List<ServerError> { problem };
                return OperationResult.Fail(503, problems);
            }

        }
    }
}
