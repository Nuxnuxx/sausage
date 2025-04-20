using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.LogementUseCases.Get;

public class GetLogementUseCase(IRepositoryFactory factory)
{
    public async Task<Logement?> ExecuteAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(factory.LogementRepository());
        
        return await factory.LogementRepository().FindAsync(id);
    }
}