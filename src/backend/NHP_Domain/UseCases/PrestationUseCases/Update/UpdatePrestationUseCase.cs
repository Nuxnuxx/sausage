using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Update;

public class UpdatePrestationUseCase(IRepositoryFactory factory)
{
    public async Task<Prestation?> ExecuteAsync(Prestation prestation)
    {
        await CheckBusinessRules(prestation);
        await factory.PrestationRepository().UpdateAsync(prestation);
        await factory.PrestationRepository().SaveChangesAsync();
        return prestation;
    }

    private async Task CheckBusinessRules(Prestation prestation)
    {
        ArgumentNullException.ThrowIfNull(prestation);
        ArgumentNullException.ThrowIfNull(prestation.NomPrestation);
        ArgumentNullException.ThrowIfNull(factory);
    }
}