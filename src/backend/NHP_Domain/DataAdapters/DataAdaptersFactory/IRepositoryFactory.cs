namespace NHP_Domain.DataAdapters.DataAdaptersFactory;

public interface IRepositoryFactory
{
    // Repositories de l'application
    IAdresseRepository AdresseRepository();
    IAvisRepository AvisRepository();
    IImageRepository ImageRepository();
    ILogementRepository LogementRepository();
    IPrestationRepository PrestationRepository();
    IPrestationProposeeRepository PrestationProposeeRepository();
    IRemiseRepository RemiseRepository();
    IReservationRepository ReservationRepository();
    ITypeDestinationRepository TypeDestinationRepository();
    IUtilisateurRepository UtilisateurRepository();
    IApplicationUtilisateurRepository ApplicationUtilisateurRepository();
    IApplicationRoleRepository ApplicationRoleRepository();
    ITransactionRepository TransactionRepository();
    
    // Méthode de la gestion de datasources
    Task EnsureDeleteAsync();
    Task EnsureCreatedAsync();
    Task SaveChangesAsync();
}