using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PFM.Application.UseCases.Resault;
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
        private readonly ITransactionRepository _tr;
        private readonly IUnitOfWork _uow;

        public ImportTransactionsCommandHandler(ITransactionRepository tr, IUnitOfWork uow)
        {
            _tr = tr;
            _uow = uow;
        }


        public async Task<OperationResult> Handle(ImportTransactionsCommand request, CancellationToken cancellationToken)
        {
            var valid = new List<TransactionCsv>();

            foreach (var r in request.Transactions)
            {
                if (string.IsNullOrWhiteSpace(r.Id)
                    || !DateTime.TryParseExact(r.Date.Trim(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                    || string.IsNullOrWhiteSpace(r.Direction)
                    || !double.TryParse(r.Amount.Replace("€", string.Empty).Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out _)
                    || string.IsNullOrWhiteSpace(r.Currency)
                    || string.IsNullOrWhiteSpace(r.Kind))
                    continue;
                valid.Add(r);
            }

            if (!valid.Any())
                return OperationResult.Fail("0 valid rows");

            try
            {
                foreach (var row in valid)
                {
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

                    _tr.Add(tx);
                }

                await _uow.SaveChangesAsync(cancellationToken);
                return OperationResult.Success();

            }
            catch (DbUpdateException dbEx)
            {
                return OperationResult.Fail("Database error while importing categories: " + dbEx.Message);
            }
            catch (NpgsqlException npgEx)
            {
                return OperationResult.Fail("PostgreSQL error while importing categories: " + npgEx.Message);
            }

        }
    }
}
