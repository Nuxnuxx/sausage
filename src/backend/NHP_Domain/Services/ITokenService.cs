namespace NHP_Domain.Services;

public interface ITokenService
{
    string GenerateTokenAsync(string userId, string email, string role);
}