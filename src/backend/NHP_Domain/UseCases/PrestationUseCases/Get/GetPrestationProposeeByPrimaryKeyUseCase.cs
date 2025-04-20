using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Get;

public class GetPrestationProposeeByPrimaryKeyUseCase(IRepositoryFactory factory)
{
    public async Task<PrestationProposee?> ExecuteAsync(long idPrestationProposee)
    {
        await CheckBusinessRules();
        return await factory.PrestationProposeeRepository().FindAsync(idPrestationProposee);
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IPrestationProposeeRepository prestationProposeeRepository = factory.PrestationProposeeRepository();
        ArgumentNullException.ThrowIfNull(prestationProposeeRepository);
    }
}