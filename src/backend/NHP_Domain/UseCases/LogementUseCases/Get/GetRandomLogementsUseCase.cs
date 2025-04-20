using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.LogementUseCases.Get;

public class GetRandomLogementsUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<List<LogementDto>> ExecuteAsync(int count)
    {
        var logements = await repositoryFactory.LogementRepository().FindAllAsync();
        var random = new Random();
        
        return new LogementDto().ToDtos(logements
            .OrderBy(x => random.Next())
            .Take(count)
            .ToList());
    }
} 