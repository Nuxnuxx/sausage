using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Services;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Create;

public class CreateApplicationRoleUseCase(IRepositoryFactory factory, IRoleManagementService roleManagementService)
{
    public async Task ExecuteAsync(string nom)
    {
        await CheckBusinessRules(nom);
        await roleManagementService.CreateRoleAsync(nom);
    }
    
    private async Task CheckBusinessRules(string role)
    {
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(factory);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals("Admin") || role.Equals("Agent");
    }
}