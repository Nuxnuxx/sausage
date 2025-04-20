using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NHP_Domain.Entities;
using NHP_Domain.Services;
using NHP_EFDataProvider.Entities;

namespace NHP_EFDataProvider.Services;

public class RoleManagementService(RoleManager<ApplicationRole> roleManager) : IRoleManagementService
{

    public async Task<(bool succeeded, IEnumerable<string> errors)> CreateRoleAsync(string roleName)
    {
        var applicationRole = new ApplicationRole(){
            Name = roleName
        };
        var result = await roleManager.CreateAsync(applicationRole);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> DeleteRoleAsync(string roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId);
        if (role == null)
            return (false, new[] { "Rôle non trouvé" });

        var result = await roleManager.DeleteAsync(role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool succeeded, IEnumerable<string> errors)> UpdateRoleAsync(string roleId, string roleName)
    {
        var role = await roleManager.FindByIdAsync(roleId);
        if (role == null)
            return (false, new[] { "Rôle non trouvé" });

        role.Name = roleName;
        var result = await roleManager.UpdateAsync(role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await roleManager.RoleExistsAsync(roleName);
    }

    public List<IApplicationRole> GetAllRoles()
    {
        List<ApplicationRole> roles = roleManager.Roles.ToList();
        List<IApplicationRole> applicationRoles = new List<IApplicationRole>();
        foreach (var role in roles)
        {
            applicationRoles.Add(new ApplicationRole
            {
                Id = role.Id,
                Name = role.Name
            });
        }
        return applicationRoles;
    }

    public async Task<string> GetRoleIdByNameAsync(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        return role.Id;
    }
}