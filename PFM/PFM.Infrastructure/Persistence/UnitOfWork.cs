

using PFM.Domain.Interfaces;
using PFM.Infrastructure.Persistence.DbContexts;

namespace PFM.Infrastructure.Persistence;


    public class UnitOfWork : IUnitOfWork
    {
        private readonly PFMDbContext _context;

        public UnitOfWork(PFMDbContext context)
        {
            _context = context;
    
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
