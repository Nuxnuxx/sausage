using System.Collections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NHP_Domain.DataAdapters;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.RoleExceptions;
using NHP_EFDataProvider.Data;
using NHP_EFDataProvider.Entities;

namespace NHP_EFDataProvider.Repositories;

public class ApplicationRoleRepository(NhpDbContext context, RoleManager<ApplicationRole> roleManager) : 
    Repository<IApplicationRole>(context), IApplicationRoleRepository
{
    public Task<IApplicationRole?> FindRoleByIdAsync(string id)
    {
        roleManager.FindByIdAsync(id);
        return Task.FromResult<IApplicationRole?>(null);
    }

    public async Task<IEnumerable<IApplicationRole>> FindAllRolesAsync()
    {
        return await Context.ApplicationRoles.ToListAsync();
    }

    public async Task AddRoleAsync(string role)
    {
        string roleId = Guid.NewGuid().ToString();
        await roleManager.CreateAsync(new ApplicationRole(){Name = role, Id = roleId}); 
    }
    
    public async Task UpdateRoleAsync(IApplicationRole role)
    {
        var applicationRole = await roleManager.FindByIdAsync(role.RoleId.ToString());
        if (applicationRole == null)
            throw new RoleNotFoundException(role.RoleId.ToString());
        
        applicationRole.Name = role.Role.Nom;
        await roleManager.UpdateAsync(applicationRole);
    }

    public async Task<IApplicationRole?> FindByNameAsync(string role)
    {
        return await roleManager.FindByNameAsync(role);
    }
    
    
    
}