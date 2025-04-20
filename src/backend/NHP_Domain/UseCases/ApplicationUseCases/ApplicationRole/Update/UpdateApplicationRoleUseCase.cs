using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Update;

public class UpdateApplicationRoleUseCase(IRepositoryFactory factory)
{
    
    public async Task<bool> ExecuteAsync(string id, string nom)
    {
        await CheckBusinessRules();
        IApplicationRole? applicationRole = await factory.ApplicationRoleRepository().FindRoleByIdAsync(id);
        if (applicationRole == null)
            return false;
        
        applicationRole.Role.Nom = nom;
        
        await factory.ApplicationRoleRepository().UpdateRoleAsync(applicationRole);
        return true;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(factory.ApplicationRoleRepository());
        
    }
    
}