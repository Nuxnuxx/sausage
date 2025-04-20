namespace NHP_Domain.Exceptions.AvisExceptions;

public class InvalidAvisException : Exception
{
    public InvalidAvisException() : base() { }
    public InvalidAvisException(string message) : base(message) { }
    public InvalidAvisException(string message, Exception innerException) : base(message, innerException) { }
}