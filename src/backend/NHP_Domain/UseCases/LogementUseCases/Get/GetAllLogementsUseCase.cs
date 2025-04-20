using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using System.Linq;

namespace NHP_Domain.UseCases.LogementUseCases.Get;

public class GetAllLogementsUseCase(IRepositoryFactory factory)
{
    public async Task<List<Logement>> ExecuteAsync(string? nomLogement = null, string? destinationNom = null)
    {
        ArgumentNullException.ThrowIfNull(factory.LogementRepository());
        
        if (!string.IsNullOrEmpty(nomLogement) && !string.IsNullOrEmpty(destinationNom))
        {
            // Filter by both name and destination
            return await factory.LogementRepository().FindByConditionAsync(l => 
                l.NomLogement != null && 
                l.NomLogement.Contains(nomLogement, StringComparison.OrdinalIgnoreCase) &&
                l.Destination != null && 
                l.Destination.NomTypeDestination != null && 
                l.Destination.NomTypeDestination.Contains(destinationNom, StringComparison.OrdinalIgnoreCase));
        }
        else if (!string.IsNullOrEmpty(nomLogement))
        {
            // Filter by name only
            return await factory.LogementRepository().FindByConditionAsync(l => 
                l.NomLogement != null && 
                l.NomLogement.Contains(nomLogement, StringComparison.OrdinalIgnoreCase));
        }
        else if (!string.IsNullOrEmpty(destinationNom))
        {
            // Filter by destination only
            return await factory.LogementRepository().FindByConditionAsync(l => 
                l.Destination != null && 
                l.Destination.NomTypeDestination != null && 
                l.Destination.NomTypeDestination.Contains(destinationNom, StringComparison.OrdinalIgnoreCase));
        }
        else
        {
            // No filters applied
            return await factory.LogementRepository().FindAllAsync();
        }
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        ILogementRepository logementRepository = factory.LogementRepository();
        ArgumentNullException.ThrowIfNull(logementRepository);
    }
}