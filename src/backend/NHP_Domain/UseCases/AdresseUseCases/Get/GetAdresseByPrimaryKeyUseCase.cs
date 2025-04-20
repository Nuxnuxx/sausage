using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AdresseUseCases.Get;

public class GetAdresseByPrimaryKeyUseCase(IRepositoryFactory factory)
{
    public async Task<Adresse?> ExecuteAsync(string nomAdresse)
    {
        await CheckBusinessRules();
        return await factory.AdresseRepository().FindAsync(nomAdresse);
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IAdresseRepository adresseRepository = factory.AdresseRepository();
        ArgumentNullException.ThrowIfNull(adresseRepository);
    }
}