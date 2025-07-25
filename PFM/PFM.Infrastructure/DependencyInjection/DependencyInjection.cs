using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PFM.Application.Interfaces;
using PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics;
using PFM.Application.UseCases.Transaction.Queries.GetAllTransactions;
using PFM.Application.Validation;
using PFM.Domain.Interfaces;
using PFM.Infrastructure.Persistence;
using PFM.Infrastructure.Persistence.Repositories;
using PFM.Infrastructure.Services;
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
            services.AddScoped<IValidator<GetSpendingsAnalyticsQuery>, GetSpendingsAnalyticsQueryValidator>();
            services.AddScoped<ITransactionImportLogger, FileTransactionImportLogger>();
            services.AddScoped<IAutoCategorizationService, AutoCategorizationService>();
            return services;
        }

       
    }
}
