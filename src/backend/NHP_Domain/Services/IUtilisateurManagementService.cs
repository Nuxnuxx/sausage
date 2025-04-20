using NHP_Domain.Entities;

namespace NHP_Domain.Services;

public interface IUtilisateurManagementService
{
    Task<(bool succeeded, IEnumerable<string> errors)> CreateUserAsync(string email, string userName, string password, string utilisateurId);
    Task<(bool succeeded, IEnumerable<string> errors)> DeleteUserAsync(string userId);
    Task<(bool succeeded, IEnumerable<string> errors)> AddToRoleAsync(string userId, string role);
    Task<(bool succeeded, IEnumerable<string> errors)> RemoveFromRoleAsync(string userId, string role);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<string> GetUserIdByEmailAsync(string email);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task<(bool succeeded, IEnumerable<string> errors)> UpdateUserAsync(string userId, string email, string userName);
    Task<(bool succeeded, IEnumerable<string> errors)> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<IEnumerable<IApplicationUtilisateur>> GetUsersByRoleAsync(string role);
}