namespace NHP_Domain.Services;

public interface IAuthenticationService
{
    Task<(bool succeeded, string userId)> SignInAsync(string email, string password, bool rememberMe = false);
    Task SignOutAsync();
    Task<bool> ValidateCredentialsAsync(string email, string password);
    Task<string> GenerateTwoFactorTokenAsync(string userId, string provider);
    Task<bool> ValidateTwoFactorTokenAsync(string userId, string provider, string token);
    Task<bool> IsLockedOutAsync(string userId);
    Task<(bool succeeded, IEnumerable<string> errors)> ResetPasswordAsync(string userId, string token, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(string userId);
}