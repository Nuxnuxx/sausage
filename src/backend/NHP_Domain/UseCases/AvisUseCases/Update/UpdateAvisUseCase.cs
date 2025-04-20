using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.AvisExceptions;

namespace NHP_Domain.UseCases.AvisUseCases.Update;

public class UpdateAvisUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<Avis> ExecuteAsync(Avis avis)
    {
        await CheckBusinessRules(avis);
        await repositoryFactory.AvisRepository().UpdateAsync(avis);
        await repositoryFactory.SaveChangesAsync();
        return avis;
    }

    private async Task CheckBusinessRules(Avis avis)
    {
        ArgumentNullException.ThrowIfNull(avis);
        ArgumentNullException.ThrowIfNull(avis.Logement);
        ArgumentNullException.ThrowIfNull(avis.Utilisateur);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AvisRepository());
        
        // Vérifie que la note de l'avis est valide (entre 1 et 5)
        if (avis.NoteAvis < 1 || avis.NoteAvis > 5)
            throw new InvalidAvisException("La note doit être comprise entre 1 et 5.");
        
        // Si un texte est fourni, il doit contenir au moins 5 caractères
        if (avis.TexteAvis is not null && avis.TexteAvis.Length < 5)
            throw new TooShortException("Le texte de l'avis doit contenir au moins 5 caractères s'il est fourni.");

        // Vérifie si le logement existe dans la base de données
        var logement = await repositoryFactory.LogementRepository().FindAsync(avis.Logement.NomLogement);
        if (logement == null)
            throw new NotFoundException("Le logement spécifié n'existe pas.");

        // Vérifie si l'utilisateur existe dans la base de données
        var utilisateur = await repositoryFactory.UtilisateurRepository().FindAsync(avis.Utilisateur.MailUtilisateur);
        if (utilisateur == null)
            throw new NotFoundException("L'utilisateur spécifié n'existe pas.");
    }
}