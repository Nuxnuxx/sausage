using Microsoft.AspNetCore.Identity;
using NHP_Domain.Entities;

namespace NHP_EFDataProvider.Entities;

public class ApplicationRole: IdentityRole, IApplicationRole
{
    [PersonalData]
    public long? RoleId { get; set; }
    [PersonalData]
    public virtual Role? Role { get; set; }
    
}