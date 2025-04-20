using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Get;

public class GetAllApplicationUtilisateursUseCase(IRepositoryFactory factory)
{
    public async Task<IEnumerable<IApplicationUtilisateur>> ExecuteAsync()
    {
        await CheckBusinessRules();

        var utilisateurs = await factory.ApplicationUtilisateurRepository().GetAllAsync();
        if (utilisateurs == null)
            throw new ArgumentNullException("Aucun utilisateur trouvé");
        return utilisateurs;
    }
    
    public async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory.ApplicationUtilisateurRepository());
        ArgumentNullException.ThrowIfNull(factory.UtilisateurRepository());
    }
}