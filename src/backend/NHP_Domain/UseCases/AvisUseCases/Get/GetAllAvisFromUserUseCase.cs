using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.AvisExceptions;

namespace NHP_Domain.UseCases.AvisUseCases.Get;

public class GetAvisFromUserUseCase(IRepositoryFactory repositoryFactory)
{
    // Récupère la liste des avis d'un utilisateur
    public async Task<List<Avis>> ExecuteAsync(string mailUtilisateur)
    {
        await CheckBusinessRules(mailUtilisateur);
        return await repositoryFactory.AvisRepository().FindByConditionAsync(a => a.Utilisateur.MailUtilisateur == mailUtilisateur);
    }

    private async Task CheckBusinessRules(string mailUtilisateur)
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AvisRepository());
        
        var utilisateur = await repositoryFactory.UtilisateurRepository().FindAsync(mailUtilisateur);
        if (utilisateur == null)
            throw new NotFoundException("L'utilisateur spécifié n'existe pas.");
    }
}