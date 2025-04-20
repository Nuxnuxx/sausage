namespace NHP_Domain.Exceptions.AvisExceptions;

public class TooShortException : Exception
{
    public TooShortException() : base() { }
    public TooShortException(string message) : base(message) { }
    public TooShortException(string message, Exception innerException) : base(message, innerException) { }
}