namespace NHP_Domain.Exceptions.UtilisateurExceptions;

public class AlreadyUsedEmailException : Exception
{
    public AlreadyUsedEmailException() : base() { }
    public AlreadyUsedEmailException(string message) : base(message) { }
    public AlreadyUsedEmailException(string message, Exception innerException) : base(message, innerException) { }
}