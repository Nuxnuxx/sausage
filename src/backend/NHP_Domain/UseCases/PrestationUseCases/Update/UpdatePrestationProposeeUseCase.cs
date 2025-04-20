using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Update;

public class UpdatePrestationProposeeUseCase(IRepositoryFactory factory)
{
    public async Task<PrestationProposee?> ExecuteAsync(PrestationProposee prestationProposee)
    {
        await CheckBusinessRules(prestationProposee);
        await factory.PrestationProposeeRepository().UpdateAsync(prestationProposee);
        await factory.PrestationProposeeRepository().SaveChangesAsync();
        return prestationProposee;
    }

    private async Task CheckBusinessRules(PrestationProposee prestationProposee)
    {
        ArgumentNullException.ThrowIfNull(prestationProposee);
        ArgumentNullException.ThrowIfNull(prestationProposee.PrixPrestation);
        ArgumentNullException.ThrowIfNull(prestationProposee.IdPrestationProposee);
        ArgumentNullException.ThrowIfNull(factory);
    }
}
