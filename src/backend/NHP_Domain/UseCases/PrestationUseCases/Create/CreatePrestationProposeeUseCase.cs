using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Create;

public class CreatePrestationProposeeUseCase(IRepositoryFactory factory)
{
    public async Task<PrestationProposee> ExecuteAsync(long idPrestation, long prixPrestation)
    {
        PrestationProposee pps = new PrestationProposee
        {
            IdPrestationProposee = idPrestation,
            PrixPrestation = prixPrestation,
        };
        return await ExecuteAsync(pps);
    }

    public async Task<PrestationProposee> ExecuteAsync(PrestationProposee prestationProposee)
    {
        await CheckBusinessRules(prestationProposee);
        await factory.PrestationProposeeRepository().CreateAsync(prestationProposee);
        await factory.PrestationProposeeRepository().SaveChangesAsync();
        return prestationProposee;
    }

    private async Task CheckBusinessRules(PrestationProposee prestationProposee)
    {
        ArgumentNullException.ThrowIfNull(prestationProposee);
        ArgumentNullException.ThrowIfNull(prestationProposee.IdPrestationProposee);
        ArgumentNullException.ThrowIfNull(prestationProposee.PrixPrestation);
        ArgumentNullException.ThrowIfNull(factory);
    }
}