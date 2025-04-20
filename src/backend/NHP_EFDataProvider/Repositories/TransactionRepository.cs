using NHP_Domain.DataAdapters;
using NHP_EFDataProvider.Data;

namespace NHP_EFDataProvider.Repositories;

public class TransactionRepository(NhpDbContext context) : ITransactionRepository
{
    public async Task BeginTransactionAsync()
    {
        context.Database.BeginTransaction();
    }

    public async Task CommitTransactionAsync()
    {
        context.Database.CommitTransaction();
    }

    public async Task RollbackTransactionAsync()
    {
        context.Database.RollbackTransaction();
    }
}