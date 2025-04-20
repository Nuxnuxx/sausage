using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Get;

public class GetTouteLesPrestationsProposeeUseCase(IRepositoryFactory factory)
{
    public async Task<List<PrestationProposee>> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<PrestationProposee> prestationsProposees = await factory.PrestationProposeeRepository().FindAllAsync();
        return prestationsProposees;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IPrestationProposeeRepository prestationProposeeRepository = factory.PrestationProposeeRepository();
        ArgumentNullException.ThrowIfNull(prestationProposeeRepository);
    }
}