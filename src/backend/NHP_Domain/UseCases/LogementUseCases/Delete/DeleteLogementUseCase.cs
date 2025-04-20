using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.LogementUseCases.Delete;

public class DeleteLogementUseCase(IRepositoryFactory factory)
{
    public async Task<bool> ExecuteAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(factory.LogementRepository());

        Logement? logement = await factory.LogementRepository().FindAsync(id);
        if (logement == null)
            return false;
            
        await factory.LogementRepository().DeleteAsync(logement);
        factory.LogementRepository().SaveChangesAsync().Wait();
        return true;
    }
}