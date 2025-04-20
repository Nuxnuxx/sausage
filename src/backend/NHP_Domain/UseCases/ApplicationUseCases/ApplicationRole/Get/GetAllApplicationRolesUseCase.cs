using System.Collections;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Services;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Get;

public class GetAllApplicationRolesUseCase(IRepositoryFactory factory, IRoleManagementService roleManagementService)
{
    
    public async Task<IEnumerable> ExecuteAsync()
    {
        await CheckBusinessRules();
        //var roles = roleManagementService.GetAllRoles();
        var roles = await factory.ApplicationRoleRepository().FindAllRolesAsync();
        return roles;
    }

    public async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
    }
    
    
    
}