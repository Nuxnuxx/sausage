using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.UtilisateurExceptions;
using NHP_Domain.Services;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Create;

public class CreateUtilisateurWithRoleUseCase(
    IRepositoryFactory factory, 
    IUtilisateurManagementService utilisateurManagementService,
    IRoleManagementService roleManagementService)
{

    public async Task<(Utilisateur utilisateur, bool success, IEnumerable<string> errors)> ExecuteAsync(
        string email,
        string password,
        string nom,
        string prenom,
        string role = "Client",
        string? civilite = null,
        string? telephone = null,
        bool newsletter = false)
    {
        await factory.TransactionRepository().BeginTransactionAsync();
        
        // Vérifier si le rôle existe, sinon le créer
        if (!await roleManagementService.RoleExistsAsync(role))
        {
            var (succeeded, errors) = await roleManagementService.CreateRoleAsync(role);
            if (!succeeded)
            {
                await factory.TransactionRepository().RollbackTransactionAsync();
                return (null, false, errors);
            }
        }

        // Créer l'utilisateur
        try
        {
            //Générate a unique ID for the user
            string utilisateurId = Guid.NewGuid().ToString();
            // Créer l'entité utilisateur
            var utilisateur = new Utilisateur
            {
                Id = utilisateurId,
                MailUtilisateur = email,
                NomUtilisateur = nom,
                PrenomUtilisateur = prenom,
                CiviliteUtilisateur = civilite,
                TelephoneUtilisateur = telephone,
                NewsLetterUtilisateur = newsletter
            };
            
            Utilisateur createdUtilisateur = await factory.UtilisateurRepository().CreateAsync(utilisateur);
            await factory.SaveChangesAsync();
            
            // Créer l'utilisateur Identity
            var (userCreated, userErrors) = await utilisateurManagementService.CreateUserAsync(
                email, 
                email, // Utiliser l'email comme nom d'utilisateur
                password, 
                utilisateurId);
                
            
            
            if (userCreated)
            {
                // Obtenir l'ID de l'utilisateur Identity
                string userId = await utilisateurManagementService.GetUserIdByEmailAsync(email);
                
                // Ajouter l'utilisateur au rôle
                var (roleAdded, roleErrors) = await utilisateurManagementService.AddToRoleAsync(userId, role);
                
                if (roleAdded)
                {
                    await factory.TransactionRepository().CommitTransactionAsync();
                    return (createdUtilisateur, true, Array.Empty<string>());
                }
                await factory.TransactionRepository().CommitTransactionAsync();
                return (createdUtilisateur, false, roleErrors);
            }
            await factory.TransactionRepository().RollbackTransactionAsync();
            return (utilisateur, false, userErrors);
        }
        catch (Exception ex)
        {
            await factory.TransactionRepository().RollbackTransactionAsync();
            return (null, false, new[] { ex.Message });
        }
    }
}