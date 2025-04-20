using NHP_Domain.Entities;

namespace NHP_Domain.DataAdapters;

public interface IUtilisateurRepository : IRepository<Utilisateur>
{
    Task<Utilisateur?> GetByEmailAsync(string email);
}