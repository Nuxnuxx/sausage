using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Get;

public class GetTouteLesPrestationsUseCase(IRepositoryFactory factory)
{
    public async Task<List<Prestation>> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Prestation> prestations = await factory.PrestationRepository().FindAllAsync();
        return prestations;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IPrestationRepository prestationRepository = factory.PrestationRepository();
        ArgumentNullException.ThrowIfNull(prestationRepository);
    }
}