using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AvisUseCases.Delete;

public class DeleteAvisUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<bool> ExecuteAsync(string mailUtilisateur, string nomLogement)
    {
        await CheckBusinessRules();
       
        var avis = await repositoryFactory.AvisRepository()
            .FindByConditionAsync(a => a.Utilisateur.MailUtilisateur == mailUtilisateur && a.Logement.NomLogement == nomLogement);

        if (!avis.Any())
        {
            return false;
        }
        
        await repositoryFactory.AvisRepository().DeleteAsync(avis.First());

        return true;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AvisRepository());
    }
}