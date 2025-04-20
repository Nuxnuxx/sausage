using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AdresseUseCases.Get;

public class GetTouteLesAdressesUseCase(IRepositoryFactory factory)
{
    public async Task<List<Adresse>> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Adresse> adresses = await factory.AdresseRepository().FindAllAsync();
        return adresses;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IAdresseRepository adresseRepository = factory.AdresseRepository();
        ArgumentNullException.ThrowIfNull(adresseRepository);
    }
    
}