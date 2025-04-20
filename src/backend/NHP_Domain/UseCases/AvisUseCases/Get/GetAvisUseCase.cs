using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.AvisExceptions;

namespace NHP_Domain.UseCases.AvisUseCases.Get;

public class GetAvisUseCase(IRepositoryFactory repositoryFactory)
{
    // Récupère un avis spécifique d'un utilisateur pour un logement
    public async Task<Avis?> ExecuteAsync(string nomLogement, string mailUtilisateur)
    {
        await CheckBusinessRules(nomLogement, mailUtilisateur);
        var avis = await repositoryFactory.AvisRepository()
            .FindByConditionAsync(a => a.Logement.NomLogement == nomLogement && a.Utilisateur.MailUtilisateur == mailUtilisateur);

        return avis.FirstOrDefault(); // Si plusieurs avis sont renvoyés (théoriquement ce ne devrait pas être le cas), on prend le premier
    }

    private async Task CheckBusinessRules(string nomLogement, string mailUtilisateur)
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AvisRepository());
        
        var logement = await repositoryFactory.LogementRepository().FindAsync(nomLogement);
        if (logement == null)
            throw new NotFoundException("Le logement spécifié n'existe pas.");

        var utilisateur = await repositoryFactory.UtilisateurRepository().FindAsync(mailUtilisateur);
        if (utilisateur == null)
            throw new NotFoundException("L'utilisateur spécifié n'existe pas.");
    }
}