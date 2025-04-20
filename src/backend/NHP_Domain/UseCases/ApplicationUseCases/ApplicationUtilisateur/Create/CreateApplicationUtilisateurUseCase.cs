using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Util;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Create;

public class CreateApplicationUtilisateurUseCase(IRepositoryFactory factory)
{
    public async Task<IApplicationUtilisateur> ExecuteAsync(string login, string email, string password, string role, Utilisateur utilisateur)
    {
        await CheckBusinessRules(email, password, utilisateur);
        return await factory.ApplicationUtilisateurRepository().AddUserAsync(login, email, password, role, utilisateur) 
               ?? throw new ArgumentNullException("L'utilisateur n'a pas pu être créé");
    }
    
    public async Task<IApplicationUtilisateur> ExecuteAsync(Utilisateur utilisateur, string password, string role)
    {
        string login = utilisateur.MailUtilisateur.Split('@')[0];
        return await ExecuteAsync(
            login, utilisateur.MailUtilisateur, password, role, utilisateur);
    }

    private async Task CheckBusinessRules(string email, string password, Utilisateur utilisateur)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(utilisateur);
        ArgumentNullException.ThrowIfNull(factory.ApplicationUtilisateurRepository());
        
        // Vérification additionnelle si un utilisateur est lié
        var existingUser = await factory.UtilisateurRepository().FindAsync(utilisateur.Id);
            if (existingUser == null)
                throw new ArgumentException("L'utilisateur associé n'existe pas");
        // Vérification si l'email existe déjà
        var existingEmail = await factory.ApplicationUtilisateurRepository().FindByEmailAsync(email);
        if (existingEmail != null)
            throw new ArgumentException("L'email existe déjà");
    }
}