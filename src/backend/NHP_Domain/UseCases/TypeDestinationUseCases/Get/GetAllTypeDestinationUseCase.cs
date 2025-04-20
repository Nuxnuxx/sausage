using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.TypeDestinationUseCases.Get;

public class GetAllTypeDestinationUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<List<TypeDestination>> ExecuteAsync()
    {
        await CheckBusinessRules();
        return await repositoryFactory.TypeDestinationRepository().FindAllAsync();
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.TypeDestinationRepository());
    }
}