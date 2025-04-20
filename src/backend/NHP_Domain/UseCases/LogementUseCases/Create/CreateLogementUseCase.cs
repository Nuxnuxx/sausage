using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.LogementUseCases.Create;

public class CreateLogementUseCase(IRepositoryFactory factory)
{
    public async Task<Logement> ExecuteAsync(String nomLogement, long prixLogement, String descriptionLogement, 
        Image? image = null, TypeDestination? destination = null, Adresse? adresse = null)
    {
        var logement = new Logement
        {
            NomLogement = nomLogement,
            PrixLogement = prixLogement,
            DescriptionLogement = descriptionLogement,
            Image = image,
            Destination = destination,
            Adresse = adresse
        };
        
        return await ExecuteAsync(logement);
    }

    public async Task<Logement> ExecuteAsync(Logement logement)
    {
        await CheckBusinessRules(logement);
        Logement result = await factory.LogementRepository().CreateAsync(logement);
        factory.LogementRepository().SaveChangesAsync().Wait();
        return result;
    }

    private async Task CheckBusinessRules(Logement logement)
    {
        ArgumentNullException.ThrowIfNull(logement);
        ArgumentNullException.ThrowIfNull(logement.NomLogement);
        ArgumentNullException.ThrowIfNull(logement.DescriptionLogement);
        
        if (logement.PrixLogement <= 0)
            throw new ArgumentException("Le prix du logement doit être supérieur à 0");
            
        ArgumentNullException.ThrowIfNull(factory.LogementRepository());
    }
}