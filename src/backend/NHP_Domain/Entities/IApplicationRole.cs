namespace NHP_Domain.Entities;

public interface IApplicationRole
{
    long? RoleId { get; set; }
    Role? Role { get; set; }
}