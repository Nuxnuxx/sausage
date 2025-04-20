namespace NHP_Domain.Exceptions.AvisExceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base() { }
    public UnauthorizedException(string message) : base(message) { }
    public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
}