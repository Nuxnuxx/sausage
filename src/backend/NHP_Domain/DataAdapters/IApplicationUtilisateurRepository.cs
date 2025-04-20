using NHP_Domain.Entities;

namespace NHP_Domain.DataAdapters;

public interface IApplicationUtilisateurRepository
{
    Task<IApplicationUtilisateur?> AddUserAsync(string login, string email, string password, string role, Utilisateur utilisateur);
    Task<IApplicationUtilisateur?> FindByEmailAsync(string email);
    Task<IApplicationUtilisateur?> FindByIdAsync(string id);
    Task<IEnumerable<IApplicationUtilisateur>> GetAllAsync();
    Task<bool> IsInRoleAsync(string email, string role);
    Task UpdateAsync(Utilisateur utilisateur);
    Task<bool> DeleteAsync(string email);
}