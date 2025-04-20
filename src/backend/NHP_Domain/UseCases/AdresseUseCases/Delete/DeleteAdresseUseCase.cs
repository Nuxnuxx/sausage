using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AdresseUseCases.Delete;

public class DeleteAdresseUseCase(IRepositoryFactory factory)
{
    public async Task<bool> ExecuteAsync(Adresse adresse)
    {
        await CheckBusinessRules();
        await factory.AdresseRepository().DeleteAsync(adresse);
        return true;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IAdresseRepository adresseRepository = factory.AdresseRepository();
        ArgumentNullException.ThrowIfNull(adresseRepository);
    }
    
    
}