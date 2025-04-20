using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.AvisExceptions;

namespace NHP_Domain.UseCases.AvisUseCases.Create;

public class CreateAvisUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<Avis> ExecuteAsync(int noteAvis, string? texteAvis, string nomLogement, string mailUtilisateur)
    {
        var logement = await repositoryFactory.LogementRepository().FindByConditionAsync(l => l.NomLogement == nomLogement);
        if (logement == null || !logement.Any())
            throw new NotFoundException("Le logement spécifié n'existe pas.");
        
        var utilisateur = await repositoryFactory.UtilisateurRepository().FindAsync(mailUtilisateur);
        if (utilisateur == null)
            throw new NotFoundException("L'utilisateur spécifié n'existe pas.");

        var avis = new Avis()
        {
            NoteAvis = noteAvis,
            TexteAvis = texteAvis,
            Logement = logement.First(),
            Utilisateur = utilisateur
        };
        return await ExecuteAsync(avis);
    }

    public async Task<Avis> ExecuteAsync(Avis avis)
    {
        await CheckBusinessRules(avis);
        Avis createdAvis = await repositoryFactory.AvisRepository().CreateAsync(avis);
        await repositoryFactory.AvisRepository().SaveChangesAsync();
        return createdAvis;
    }

    private async Task CheckBusinessRules(Avis avis)
    {
        ArgumentNullException.ThrowIfNull(avis);
        ArgumentNullException.ThrowIfNull(avis.Logement);
        ArgumentNullException.ThrowIfNull(avis.Utilisateur);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AvisRepository());
        
        // Vérifie si un avis existe déjà pour ce logement et cet utilisateur
        var existingAvis = await repositoryFactory.AvisRepository()
            .FindByConditionAsync(a => a.Utilisateur.MailUtilisateur == avis.Utilisateur.MailUtilisateur &&
                                       a.Logement.NomLogement == avis.Logement.NomLogement);
        
        if (existingAvis != null && existingAvis.Any())
            throw new AlreadyExistException("Un avis de cet utilisateur pour ce logement existe déjà.");
        
        // Vérifie que la note de l'avis est valide (entre 1 et 5)
        if (avis.NoteAvis < 1 || avis.NoteAvis > 5)
            throw new InvalidAvisException("La note doit être comprise entre 1 et 5.");
        
        // Si un texte est fourni, il doit contenir au moins 5 caractères
        if (avis.TexteAvis is not null && avis.TexteAvis.Length < 5)
            throw new TooShortException("Le texte de l'avis doit contenir au moins 5 caractères s'il est fourni.");

        // Vérifie si l'utilisateur a effectué une réservation dans ce logement avant de pouvoir laisser un avis
        var reservations = await repositoryFactory.ReservationRepository()
            .FindByConditionAsync(r => r.Utilisateur!.MailUtilisateur == avis.Utilisateur.MailUtilisateur &&
                                       r.Logement!.NomLogement == avis.Logement.NomLogement &&
                                       r.DepartReservation < DateTime.Now);
        
        if (reservations != null && !reservations.Any())
            throw new UnauthorizedException("L'utilisateur doit avoir une réservation passée dans ce logement pour laisser un avis.");
    }
}