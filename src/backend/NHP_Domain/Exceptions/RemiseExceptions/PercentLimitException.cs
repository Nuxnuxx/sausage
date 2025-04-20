namespace NHP_Domain.Exceptions.RemiseExceptions;

public class PercentLimitException : Exception
{
    public PercentLimitException() : base() { }
    public PercentLimitException(string message) : base(message) { }
    public PercentLimitException(string message, Exception innerException) : base(message, innerException) { }
}