using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Services;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Auth;

public class AuthenticateUtilisateurUseCase(
    IRepositoryFactory factory,
    IAuthenticationService authService,
    IUtilisateurManagementService utilisateurManager,
    ITokenService tokenService)
{

    public async Task<(bool success, Utilisateur utilisateur, IList<string> roles, string token)> ExecuteAsync(
        string email, 
        string password, 
        bool rememberMe = false)
    {
        // Authentifier l'utilisateur
        var (succeeded, userId) = await authService.SignInAsync(email, password, rememberMe);
        
        if (!succeeded || string.IsNullOrEmpty(userId))
            return (false, null, new List<string>(), null);
            
        // Récupérer les rôles de l'utilisateur
        var roles = await utilisateurManager.GetUserRolesAsync(userId);
        
        
        // Récupérer les informations de l'entité Utilisateur
        var user = await factory.UtilisateurRepository().GetByEmailAsync(email);
        
        var token =  tokenService.GenerateTokenAsync(user.Id, email, roles[0]);
        
        return (true, user, roles, token);
    }
}