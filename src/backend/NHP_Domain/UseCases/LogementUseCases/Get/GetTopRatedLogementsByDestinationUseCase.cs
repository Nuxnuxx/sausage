using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.LogementUseCases.Get;

public class GetTopRatedLogementsByDestinationUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<List<LogementWithRatingDto>> ExecuteAsync(long destinationId, int count)
    {
        var logements = await repositoryFactory.LogementRepository().FindAllAsync();
        var avis = await repositoryFactory.AvisRepository().FindAllAsync();
        
        var logementsWithAverage = logements
            .Where(l => l.Destination?.Id == destinationId)
            .Select(l => new
            {
                Logement = l,
                AverageRating = avis.Where(a => a.Logement?.Id == l.Id)
                                  .Select(a => a.NoteAvis)
                                  .DefaultIfEmpty(0)
                                  .Average()
            });
        
        var topLogements = logementsWithAverage
            .OrderByDescending(x => x.AverageRating)
            .Take(count)
            .Select(x => x.Logement)
            .ToList();
            
        return LogementWithRatingDto.ToDtos(topLogements, avis);
    }
} 