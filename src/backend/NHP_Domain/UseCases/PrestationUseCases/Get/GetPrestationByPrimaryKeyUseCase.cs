using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Get;

public class GetPrestationByPrimaryKeyUseCase(IRepositoryFactory factory)
{
    public async Task<Prestation?> ExecuteAsync(long id)
    {
        await CheckBusinessRules();
        return await factory.PrestationRepository().FindAsync(id);
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IPrestationRepository prestationRepository = factory.PrestationRepository();
        ArgumentNullException.ThrowIfNull(prestationRepository);
    }
}