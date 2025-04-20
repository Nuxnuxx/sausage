using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AdresseUseCases.Create;

public class CreateAdresseUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<Adresse> ExecuteAsync(string nomAdresse, string complementAdresse, string ville, int codePostal,
        string pays)
    {
        var adresse = new Adresse
        {
            NomAdresse = nomAdresse,
            ComplementAdresse = complementAdresse,
            Ville = ville,
            CodePostal = codePostal,
            Pays = pays
        };
        return await ExecuteAsync(adresse);
    }

    public async Task<Adresse> ExecuteAsync(Adresse adresse)
    {
        await CheckBusinessRules(adresse);
        Adresse ad = await repositoryFactory.AdresseRepository().CreateAsync(adresse);
        repositoryFactory.AdresseRepository().SaveChangesAsync().Wait();
        return ad;
    }

    private async Task CheckBusinessRules(Adresse adresse)
    {
        ArgumentNullException.ThrowIfNull(adresse);
        ArgumentNullException.ThrowIfNull(adresse.NomAdresse);
        ArgumentNullException.ThrowIfNull(adresse.ComplementAdresse);
        ArgumentNullException.ThrowIfNull(adresse.Ville);
        ArgumentNullException.ThrowIfNull(adresse.CodePostal);
        ArgumentNullException.ThrowIfNull(adresse.Pays);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AdresseRepository());
        
    }
}