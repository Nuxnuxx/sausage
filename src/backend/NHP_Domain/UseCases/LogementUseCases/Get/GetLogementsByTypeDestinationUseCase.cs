using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.LogementUseCases.Get;

public class GetLogementsByTypeDestinationUseCase(IRepositoryFactory repositoryFactory)
{

    public async Task<List<LogementDto>> ExecuteAsync(long typeDestinationId)
    {
        var logements = await repositoryFactory.LogementRepository().FindByConditionAsync(l => l.Destination.Id == typeDestinationId);
        return new LogementDto().ToDtos(logements);
    }
} 