using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AdresseUseCases.Update;

public class UpdateAdresseUseCase(IRepositoryFactory factory)
{
    public async Task<Adresse?> ExecuteAsync(Adresse adresse)
    {
        await CheckBusinessRules(adresse);
        await factory.AdresseRepository().UpdateAsync(adresse);
        await factory.SaveChangesAsync();
        return adresse;
    }

    private async Task CheckBusinessRules(Adresse adresse)
    {
        ArgumentNullException.ThrowIfNull(adresse);
        ArgumentNullException.ThrowIfNull(adresse.NomAdresse);
        ArgumentNullException.ThrowIfNull(adresse.ComplementAdresse);
        ArgumentNullException.ThrowIfNull(adresse.CodePostal);
        ArgumentNullException.ThrowIfNull(adresse.Pays);
        ArgumentNullException.ThrowIfNull(factory.AdresseRepository());
        
    }
}