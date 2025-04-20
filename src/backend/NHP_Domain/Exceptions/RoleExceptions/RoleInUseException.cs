namespace NHP_Domain.Exceptions.RoleExceptions;

public class RoleInUseException : Exception
{
    public RoleInUseException() : base() { }
    public RoleInUseException(string message) : base(message) { }
    public RoleInUseException(string message, Exception innerException) : base(message, innerException) { }
}