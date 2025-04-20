using Microsoft.AspNetCore.Identity;
using NHP_Domain.Services;
using NHP_EFDataProvider.Entities;

namespace NHP_EFDataProvider.Services;

public class AuthenticationService(
    SignInManager<ApplicationUtilisateur> signInManager,
    UserManager<ApplicationUtilisateur> userManager) : IAuthenticationService
{

    public async Task<(bool succeeded, string userId)> SignInAsync(string email, string password, bool rememberMe = false)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return (false, null);

        var result = await signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);
        return (result.Succeeded, result.Succeeded ? user.Id : null);
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<bool> ValidateCredentialsAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) return false;

        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<string> GenerateTwoFactorTokenAsync(string userId, string provider)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return null;

        return await userManager.GenerateTwoFactorTokenAsync(user, provider);
    }

    public async Task<bool> ValidateTwoFactorTokenAsync(string userId, string provider, string token)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return false;

        return await userManager.VerifyTwoFactorTokenAsync(user, provider, token);
    }

    public async Task<bool> IsLockedOutAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return false;

        return await userManager.IsLockedOutAsync(user);
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> ResetPasswordAsync(string userId, string token, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return (false, new[] { "Utilisateur non trouvé" });

        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return null;

        return await userManager.GeneratePasswordResetTokenAsync(user);
    }
}