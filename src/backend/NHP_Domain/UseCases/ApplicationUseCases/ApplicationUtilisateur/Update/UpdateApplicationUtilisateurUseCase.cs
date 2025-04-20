using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Update;

public class UpdateApplicationUtilisateurUseCase(IRepositoryFactory factory)
{
    public async Task<IApplicationUtilisateur?> ExecuteAsync(Utilisateur applicationUtilisateur)
    {
        await CheckBusinessRules(applicationUtilisateur);
        
        // Vérifier que l'objet existe avant de le mettre à jour
        IApplicationUtilisateur? existingApplicationUtilisateur = await factory.ApplicationUtilisateurRepository()
            .FindByEmailAsync(applicationUtilisateur.MailUtilisateur);
            
        if (existingApplicationUtilisateur == null || existingApplicationUtilisateur.Utilisateur == null)
            throw new ArgumentException("L'utilisateur n'existe pas");
        
        // Mettre à jour l'utilisateur
        existingApplicationUtilisateur.Utilisateur.NomUtilisateur = applicationUtilisateur.NomUtilisateur;
        existingApplicationUtilisateur.Utilisateur.PrenomUtilisateur = applicationUtilisateur.PrenomUtilisateur;
        existingApplicationUtilisateur.Utilisateur.MailUtilisateur = applicationUtilisateur.MailUtilisateur;
        existingApplicationUtilisateur.Utilisateur.CiviliteUtilisateur = applicationUtilisateur.CiviliteUtilisateur;
        existingApplicationUtilisateur.Utilisateur.TelephoneUtilisateur = applicationUtilisateur.TelephoneUtilisateur;
        existingApplicationUtilisateur.Utilisateur.NewsLetterUtilisateur = applicationUtilisateur.NewsLetterUtilisateur;
        
        await factory.ApplicationUtilisateurRepository().UpdateAsync(existingApplicationUtilisateur.Utilisateur);
        await factory.SaveChangesAsync();
        return existingApplicationUtilisateur;
    }

    private async Task CheckBusinessRules(Utilisateur applicationUtilisateur)
    {
        ArgumentNullException.ThrowIfNull(applicationUtilisateur);
        ArgumentNullException.ThrowIfNull(factory.ApplicationUtilisateurRepository());
    }
}