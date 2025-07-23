namespace PFM.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ITransactionRepository Transactions { get; }
        ICategoryRepository Categories { get; }
        Task SaveChangesAsync();
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
