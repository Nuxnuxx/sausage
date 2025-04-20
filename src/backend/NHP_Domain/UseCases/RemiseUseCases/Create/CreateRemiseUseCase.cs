using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.RemiseExceptions;

namespace NHP_Domain.UseCases.RemiseUseCases.Create;

public class CreateRemiseUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<Remise> ExecuteAsync(string nomRemise, string codeRemise, int pourcentageRemise)
    {
        var remise = new Remise()
        {
            NomRemise = nomRemise,
            CodeRemise = codeRemise,
            PourcentageRemise = pourcentageRemise,
        };
        return await ExecuteAsync(remise);
    }

    public async Task<Remise> ExecuteAsync(Remise remise)
    {
        await CheckBusinessRules(remise);
        Remise rm = await repositoryFactory.RemiseRepository().CreateAsync(remise);
        repositoryFactory.RemiseRepository().SaveChangesAsync().Wait();
        return rm;
    }

    private async Task CheckBusinessRules(Remise remise)
    {
        ArgumentNullException.ThrowIfNull(remise);
        ArgumentNullException.ThrowIfNull(remise.NomRemise);
        ArgumentNullException.ThrowIfNull(remise.CodeRemise);
        ArgumentNullException.ThrowIfNull(remise.PourcentageRemise);
        ArgumentNullException.ThrowIfNull(repositoryFactory.RemiseRepository());
        
        // Vérification si existance du code
        var existingRemise = await repositoryFactory.RemiseRepository().FindByConditionAsync(r => r.CodeRemise == remise.CodeRemise);
        if (existingRemise.Any())
            throw new AlreadyExistException("Une remise avec ce code existe déjà.");
        
        // Le code de remise doit être supérieur ou égal à 5 caractères
        if (remise.CodeRemise.Length < 5)
            throw new TooShortException("Le code remise doit contenir au moins 5 caractères.");

        // Le pourcentage de remise doit être entre 1% et 100%
        if (remise.PourcentageRemise < 1 || remise.PourcentageRemise > 100)
            throw new PercentLimitException("Le pourcentage doit être entre 1 et 100.");

        // Le nom de la remise doit être supérieur ou égal à 3 caractères
        if (remise.NomRemise.Length < 3)
            throw new TooShortException("Le nom de la remise doit contenir au moins 3 caractères.");
    }
}