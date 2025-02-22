namespace DemoApp.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {

        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
