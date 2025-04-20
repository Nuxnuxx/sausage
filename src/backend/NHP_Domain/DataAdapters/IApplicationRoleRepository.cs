using NHP_Domain.Entities;

namespace NHP_Domain.DataAdapters;

public interface IApplicationRoleRepository : IRepository<IApplicationRole>
{
    Task<IApplicationRole?> FindRoleByIdAsync(string id);
    Task<IEnumerable<IApplicationRole>> FindAllRolesAsync();
    Task AddRoleAsync(string role);
    Task UpdateRoleAsync(IApplicationRole role);
    Task<IApplicationRole?> FindByNameAsync(string role); }