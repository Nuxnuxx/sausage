using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Services;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Delete;

public class DeleteApplicationUtilisateurUseCase(
    IRepositoryFactory factory,
    IUtilisateurManagementService utilisateurManagementService
    )
{
    public async Task<bool> ExecuteAsync(string email)
    {
        IApplicationUtilisateur? applicationUtilisateur = await factory.ApplicationUtilisateurRepository().FindByEmailAsync(email);
        if (applicationUtilisateur == null || applicationUtilisateur.Utilisateur == null)
            return false;
        var result = utilisateurManagementService.DeleteUserAsync(email);
        if (result.Result.succeeded)
        {
            await factory.UtilisateurRepository().DeleteAsync(applicationUtilisateur.Utilisateur);
            return true;
        }
        return false;
    }
    
    private async Task CheckBusinessRules(IApplicationUtilisateur applicationUtilisateur)
    {
        ArgumentNullException.ThrowIfNull(factory);
        IApplicationUtilisateurRepository applicationUtilisateurRepository = factory.ApplicationUtilisateurRepository();
        ArgumentNullException.ThrowIfNull(applicationUtilisateurRepository);
        ArgumentNullException.ThrowIfNull(applicationUtilisateur);
        ArgumentNullException.ThrowIfNull(applicationUtilisateur.Utilisateur);
        ArgumentNullException.ThrowIfNull(applicationUtilisateur.Utilisateur.MailUtilisateur);
    }
}