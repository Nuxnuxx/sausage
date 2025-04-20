using NHP_Domain.DataAdapters.DataAdaptersFactory;

namespace NHP_Domain.DataSets;

public abstract class BdBuilder(IRepositoryFactory repositoryFactory)
{
    public async Task BuildDbAsync()
    {
        // Régénération de la BD
        Console.WriteLine("Régénération de la base de données");
        await RegenerateDbAsync();
        
        // Construction des données
        Console.WriteLine("Construction des données");
        
        // Création des rôles en premier
        Console.WriteLine("Construction des rôles");
        await BuildRolesAsync();
        
        // Création des utilisateurs après les rôles
        Console.WriteLine("Construction des utilisateurs");
        await BuildUsersAsync();
        
        // Création des données de base
        Console.WriteLine("Construction des types de destination");
        await BuildTypeDestinationAsync();
        Console.WriteLine("Construction des adresses");
        await BuildAdressesAsync();
        Console.WriteLine("Construction des images");
        await BuildImageAsync();
        Console.WriteLine("Construction des prestations");
        await BuildPrestationAsync();
        Console.WriteLine("Construction des prestations proposées");
        await BuildPrestationProposeeAsync();
        Console.WriteLine("Construction des logements");
        await BuildLogementAsync();
        Console.WriteLine("Construction des remises");
        await BuildRemiseAsync();
        
        // Création des données dépendantes
        Console.WriteLine("Construction des réservations");
        await BuildReservationAsync();
        Console.WriteLine("Construction des avis");
        await BuildAvisAsync();
    }
    
    protected abstract Task RegenerateDbAsync();
    protected abstract Task BuildRolesAsync();
    protected abstract Task BuildUsersAsync();
    protected abstract Task BuildAdressesAsync();
    protected abstract Task BuildAvisAsync();
    protected abstract Task BuildImageAsync();
    protected abstract Task BuildLogementAsync();
    protected abstract Task BuildPrestationAsync();
    protected abstract Task BuildPrestationProposeeAsync();
    protected abstract Task BuildRemiseAsync();
    protected abstract Task BuildReservationAsync();
    protected abstract Task BuildTypeDestinationAsync();
}