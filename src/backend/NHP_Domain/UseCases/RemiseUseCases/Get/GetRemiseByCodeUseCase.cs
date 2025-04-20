using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.RemiseUseCases.Get;

public class GetRemiseByCodeUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<Remise?> ExecuteAsync(string codeRemise)
    {
        await CheckBusinessRules();
        return await repositoryFactory.RemiseRepository().FindAsync(codeRemise);
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IRemiseRepository remiseRepository = repositoryFactory.RemiseRepository();
        ArgumentNullException.ThrowIfNull(remiseRepository);
    }
}