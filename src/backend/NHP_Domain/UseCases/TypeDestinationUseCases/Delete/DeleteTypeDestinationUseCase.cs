using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.TypeDestinationUseCases.Delete;

public class DeleteTypeDestinationUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<bool> ExecuteAsync(string nomTypeDestination)
    {
        var typeDestination = await repositoryFactory.TypeDestinationRepository().FindAsync(nomTypeDestination);

        if (typeDestination != null)
        {
            await CheckBusinessRules(typeDestination);
            await repositoryFactory.TypeDestinationRepository().DeleteAsync(typeDestination);
            await repositoryFactory.TypeDestinationRepository().SaveChangesAsync();
            return true;
        }

        return false;
    }

    private async Task CheckBusinessRules(TypeDestination typeDestination)
    {
        ArgumentNullException.ThrowIfNull(typeDestination);
        ArgumentNullException.ThrowIfNull(repositoryFactory.TypeDestinationRepository());
    }
}