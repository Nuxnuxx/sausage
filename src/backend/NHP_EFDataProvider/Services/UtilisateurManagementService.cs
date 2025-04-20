using Microsoft.AspNetCore.Identity;
using NHP_Domain.Entities;
using NHP_Domain.Services;
using NHP_EFDataProvider.Entities;

namespace NHP_EFDataProvider.Services;

public class UtilisateurManagementService(UserManager<ApplicationUtilisateur> userManager) : IUtilisateurManagementService
{
    public async Task<(bool succeeded, IEnumerable<string> errors)> CreateUserAsync(string email, string userName, string password, string utilisateurId)
    {
        var user = new ApplicationUtilisateur
        {
            Email = email,
            UserName = userName,
            UtilisateurId = utilisateurId
        };

        var result = await userManager.CreateAsync(user, password);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> DeleteUserAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) 
            return (false, new[] { "Utilisateur non trouvé" });

        var result = await userManager.DeleteAsync(user);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> AddToRoleAsync(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) 
            return (false, new[] { "Utilisateur non trouvé" });

        var result = await userManager.AddToRoleAsync(user, role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> RemoveFromRoleAsync(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) 
            return (false, new[] { "Utilisateur non trouvé" });

        var result = await userManager.RemoveFromRoleAsync(user, role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return false;

        return await userManager.IsInRoleAsync(user, role);
    }

    public async Task<string> GetUserIdByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user?.Id;
    }

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return new List<string>();

        return await userManager.GetRolesAsync(user);
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> UpdateUserAsync(string userId, string email, string userName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return (false, new[] { "Utilisateur non trouvé" });

        user.Email = email;
        user.UserName = userName;

        var result = await userManager.UpdateAsync(user);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return (false, new[] { "Utilisateur non trouvé" });

        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }
    
    public async Task<IEnumerable<IApplicationUtilisateur>> GetUsersByRoleAsync(string role)
    {
        var users = await userManager.GetUsersInRoleAsync(role);
        return users.Select(user => new ApplicationUtilisateur
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            UtilisateurId = user.UtilisateurId
        });
    }
}