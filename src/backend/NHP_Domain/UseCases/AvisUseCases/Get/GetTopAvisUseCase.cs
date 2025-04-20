using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AvisUseCases.Get;

public class GetTopAvisUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<List<AvisDto>> ExecuteAsync(int count)
    {
        var avis = await repositoryFactory.AvisRepository().FindAllAsync();
        
        return new AvisDto().ToDtos(avis
            .OrderByDescending(a => a.NoteAvis)
            .Take(count)
            .ToList());
    }
} 