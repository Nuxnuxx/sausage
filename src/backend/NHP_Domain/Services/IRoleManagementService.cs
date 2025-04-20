using NHP_Domain.Entities;

namespace NHP_Domain.Services;

public interface IRoleManagementService
{
    Task<(bool succeeded, IEnumerable<string> errors)> CreateRoleAsync(string roleName);
    Task<(bool succeeded, IEnumerable<string> errors)> DeleteRoleAsync(string roleId);
    Task<(bool succeeded, IEnumerable<string> errors)> UpdateRoleAsync(string roleId, string roleName);
    Task<bool> RoleExistsAsync(string roleName);
    List<IApplicationRole> GetAllRoles();
    Task<string> GetRoleIdByNameAsync(string roleName); 
}