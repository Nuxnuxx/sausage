using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.TypeDestinationExceptions;

namespace NHP_Domain.UseCases.TypeDestinationUseCases.Create;

public class CreateTypeDestinationUseCase(IRepositoryFactory repositoryFactory)
{
    // Constructeur sans description
    public async Task<TypeDestination> ExecuteAsync(string nomTypeDestination)
    {
        var typeDestination = new TypeDestination
        {
            NomTypeDestination = nomTypeDestination,
        };
        return await ExecuteAsync(typeDestination);
    }
    
    // Constructeur avec description
    public async Task<TypeDestination> ExecuteAsync(string nomTypeDestination, string descriptionTypeDestination)
    {
        var typeDestination = new TypeDestination
        {
            NomTypeDestination = nomTypeDestination,
            DescriptionTypeDestination = descriptionTypeDestination
        };
        return await ExecuteAsync(typeDestination);
    }

    public async Task<TypeDestination> ExecuteAsync(TypeDestination typeDestination)
    {
        await CheckBusinessRules(typeDestination);
        var createdTypeDestination = await repositoryFactory.TypeDestinationRepository().CreateAsync(typeDestination);
        await repositoryFactory.TypeDestinationRepository().SaveChangesAsync();
        return createdTypeDestination;
    }

    private async Task CheckBusinessRules(TypeDestination typeDestination)
    {
        ArgumentNullException.ThrowIfNull(typeDestination);
        ArgumentNullException.ThrowIfNull(typeDestination.NomTypeDestination);
        ArgumentNullException.ThrowIfNull(repositoryFactory.TypeDestinationRepository());

        // Vérification si existance du type de destination
        var existingTypeDestination = await repositoryFactory.TypeDestinationRepository().FindByConditionAsync(td => td.NomTypeDestination == typeDestination.NomTypeDestination);
        if (existingTypeDestination.Any())
            throw new AlreadyExistException("Une destination avec ce nom existe déjà.");
        
        // Le type de destination doit être ou égal à 3 caractères
        if (typeDestination.NomTypeDestination.Length < 3)
            throw new TooShortException("Le type de destination doit contenir au moins 3 caractères.");

        // La description de la destination doit être supérieur ou égal à 10 caractères
        if (typeDestination.DescriptionTypeDestination != null && typeDestination.DescriptionTypeDestination.Length < 10)
            throw new TooShortException("La description de la destination doit contenir un minimum de 10 caractères.");
    }
}