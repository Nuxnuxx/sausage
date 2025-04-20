using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Exceptions.RoleExceptions;
using NHP_Domain.Services;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Delete;

public class DeleteApplicationRoleUseCase(
    IRepositoryFactory factory,
    IRoleManagementService roleManagementService,
    IUtilisateurManagementService utilisateurManagementService)
{
    
    public async Task<bool> ExecuteAsync(string nom)
    {
        var role = await roleManagementService.GetRoleIdByNameAsync(nom);
        if (role == null)
            return false;
        
        var usersWithRole = await utilisateurManagementService.GetUsersByRoleAsync(role);
        if(!usersWithRole.Any())
        {
            throw new RoleInUseException("Le rôle est utilisé par un ou plusieurs utilisateurs.");
        }
        
        await roleManagementService.DeleteRoleAsync(role);
        var applicationRole = await factory.ApplicationRoleRepository().FindByNameAsync(nom);
        if (applicationRole == null)
            return false;
        await factory.ApplicationRoleRepository().DeleteAsync(applicationRole);
        return true;
    }
    
    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(factory.ApplicationRoleRepository());
    }
    
}