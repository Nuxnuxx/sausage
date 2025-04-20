using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Create;

public class CreatePrestationUseCase(IRepositoryFactory factory)
{
    public async Task<Prestation> ExecuteAsync(String nomPrestation)
    {
        Prestation ps = new Prestation
        {
            NomPrestation = nomPrestation
        };
        return await ExecuteAsync(ps);
    }
    
    public async Task<Prestation> ExecuteAsync(Prestation prestation)
    {
        await CheckBusinessRules(prestation);
        await factory.PrestationRepository().CreateAsync(prestation);
        await factory.PrestationRepository().SaveChangesAsync();
        return prestation;
    }
    
    private async Task CheckBusinessRules(Prestation prestation)
    {
        
    }
}