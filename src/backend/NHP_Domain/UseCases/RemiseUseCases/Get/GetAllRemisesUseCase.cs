using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.RemiseUseCases.Get;

public class GetAllRemisesUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<List<Remise>> ExecuteAsync()
    {
        await CheckBusinessRules();
        return await repositoryFactory.RemiseRepository().FindAllAsync();
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IRemiseRepository remiseRepository = repositoryFactory.RemiseRepository();
        ArgumentNullException.ThrowIfNull(remiseRepository);
    }
}