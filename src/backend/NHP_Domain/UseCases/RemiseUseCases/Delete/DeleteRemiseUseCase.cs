using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.RemiseUseCases.Delete;

public class DeleteRemiseUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<bool> ExecuteAsync(string codeRemise)
    {
        var remise = await repositoryFactory.RemiseRepository().FindAsync(codeRemise);
        
        if (remise != null)
        {
            await CheckBusinessRules(remise);
            await repositoryFactory.RemiseRepository().DeleteAsync(remise);
            await repositoryFactory.RemiseRepository().SaveChangesAsync();
            return true;
        }

        return false;
    }

    private async Task CheckBusinessRules(Remise remise)
    {
        ArgumentNullException.ThrowIfNull(remise);
        ArgumentNullException.ThrowIfNull(repositoryFactory.RemiseRepository());
    }
}