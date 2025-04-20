using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.LogementUseCases.Update;

public class UpdateLogementUseCase(IRepositoryFactory factory)
{
    public async Task<Logement?> ExecuteAsync(Logement logement)
    {
        await CheckBusinessRules(logement);
        
        var existingLogement = await factory.LogementRepository().FindAsync(logement.Id);
        if (existingLogement == null)
            return null;
            
        Logement updatedLogement = await factory.LogementRepository().UpdateAsync(logement);
        factory.LogementRepository().SaveChangesAsync().Wait();
        return updatedLogement;
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