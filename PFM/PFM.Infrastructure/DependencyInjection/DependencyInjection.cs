using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PFM.Application.UseCases.Transaction.Queries.GetAllTransactions;
using PFM.Application.Validation;
using PFM.Domain.Interfaces;
using PFM.Infrastructure.Persistence;
using PFM.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
           
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IValidator<GetTransactionsQuery>, GetTransactionsQueryValidator>();
            services.AddScoped<ITransactionImportLogger, FileTransactionImportLogger>();

            return services;
        }

       
    }
}
