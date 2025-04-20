namespace NHP_Domain.DataAdapters;

public interface ITransactionRepository
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}