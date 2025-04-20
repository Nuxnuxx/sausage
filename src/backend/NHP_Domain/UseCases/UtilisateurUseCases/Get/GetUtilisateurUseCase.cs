using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.UtilisateurUseCases.Get;

public class GetUtilisateurUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<UtilisateurDto?> ExecuteAsync(string id)
    {
        var utilisateurs = await repositoryFactory.UtilisateurRepository().FindByConditionAsync(u => u.Id == id);
        var utilisateur = utilisateurs.FirstOrDefault();
        return utilisateur != null ? new UtilisateurDto().ToDto(utilisateur) : null;
    }
} 