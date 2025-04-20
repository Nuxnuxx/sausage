namespace NHP_Domain.Exceptions.UtilisateurExceptions;

public class UtilisateurCreationException : Exception
{
    public UtilisateurCreationException() : base() { }

    public UtilisateurCreationException(string message) : base(message) { }

    public UtilisateurCreationException(string message, Exception innerException) : base(message, innerException) { }
}