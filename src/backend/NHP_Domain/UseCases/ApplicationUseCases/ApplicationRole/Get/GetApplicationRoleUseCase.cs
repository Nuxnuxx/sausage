using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Get;

public class GetApplicationRoleUseCase(IRepositoryFactory factory)
{
    
    public async Task<IApplicationRole> ExecuteAsync(string id)
    {
        await CheckBusinessRules();
        var applicationRole = await factory.ApplicationRoleRepository().FindRoleByIdAsync(id);
        
        if (applicationRole == null)
        {
            throw new ArgumentException($"Aucun rôle trouvé avec l'ID {id}.");
        }
        
        return applicationRole;
    }
    
    public async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
    }
}

