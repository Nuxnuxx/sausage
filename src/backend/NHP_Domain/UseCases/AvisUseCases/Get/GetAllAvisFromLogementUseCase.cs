using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.AvisExceptions;

namespace NHP_Domain.UseCases.AvisUseCases.Get;

public class GetAvisFromLogementUseCase(IRepositoryFactory repositoryFactory)
{
    // Récupère la liste des avis d'un logement
    public async Task<List<Avis>> ExecuteAsync(string nomLogement)
    {
        await CheckBusinessRules(nomLogement);
        return await repositoryFactory.AvisRepository().FindByConditionAsync(a => a.Logement.NomLogement == nomLogement);
    }

    private async Task CheckBusinessRules(string nomLogement)
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AvisRepository());
        
        var logement = await repositoryFactory.LogementRepository().FindAsync(nomLogement);
        if (logement == null)
            throw new NotFoundException("Le logement spécifié n'existe pas.");
    }
}