using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Get;

public class GetApplicationUtilisateurUseCase(IRepositoryFactory factory)
{
    public async Task<IApplicationUtilisateur> ExecuteAsync(string email)
    {
        await CheckBusinessRules(email);
        var user = await factory.ApplicationUtilisateurRepository().FindByEmailAsync(email);
        if (user == null)
            throw new ArgumentException("L'utilisateur n'existe pas");
        return user;
    }

    public async Task CheckBusinessRules(string email)
    {
        ArgumentNullException.ThrowIfNull(factory.ApplicationUtilisateurRepository());
        
        if (email == null)
            throw new ArgumentException("L'identifiant de l'utilisateur doit être supérieur à 0");
    }
}