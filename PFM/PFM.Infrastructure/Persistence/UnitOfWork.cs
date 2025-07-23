

using PFM.Domain.Interfaces;
using PFM.Infrastructure.Persistence.DbContexts;

namespace PFM.Infrastructure.Persistence;


    public class UnitOfWork : IUnitOfWork
    {
        private readonly PFMDbContext _context;
        private readonly ITransactionRepository _txRepo;
        private readonly ICategoryRepository _catRepo;

        public ITransactionRepository Transactions => _txRepo;
        public ICategoryRepository Categories => _catRepo;
        public UnitOfWork(PFMDbContext context, ITransactionRepository txRepo, ICategoryRepository catRepo)
        {
            _context = context;
            _txRepo = txRepo;
            _catRepo = catRepo;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
