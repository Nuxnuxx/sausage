using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.TypeDestinationUseCases.Get;

public class GetTypeDestinationByPrimaryKeyUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<TypeDestination?> ExecuteAsync(string nomTypeDestination)
    {
        await CheckBusinessRules();
        return await repositoryFactory.TypeDestinationRepository().FindAsync(nomTypeDestination);
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.TypeDestinationRepository());
    }
}