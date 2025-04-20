using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.CompilerServices;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_EFDataProvider.Data;
using NHP_EFDataProvider.Entities;
using NHP_EFDataProvider.Repositories;

namespace NHP_EFDataProvider.RepositoryFactories;

public class RepositoryFactory (
    NhpDbContext context,
    UserManager<ApplicationUtilisateur> _userManager,
    RoleManager<ApplicationRole> _roleManager): IRepositoryFactory
{
    private IAdresseRepository? _adresses;
    private IApplicationRoleRepository? _applicationRoles;
    private IApplicationUtilisateurRepository? _applicationUtilisateurs;
    private IAvisRepository? _avis;
    private IImageRepository? _images;
    private ILogementRepository? _logements;
    private IPrestationProposeeRepository? _prestationProposees;
    private IPrestationRepository? _prestations;
    private IRemiseRepository? _remises;
    private IReservationRepository? _reservations;
    private ITypeDestinationRepository? _typeDestinations;
    private IUtilisateurRepository? _utilisateurs;
    private ITransactionRepository? _transactions;

    public IAdresseRepository AdresseRepository()
    {
        return _adresses ??= new AdresseRepository(context ?? throw new IncompleteInitialization());
    }

    public IAvisRepository AvisRepository()
    {
        return _avis ??= new AvisRepository(context ?? throw new IncompleteInitialization());
    }

    public IImageRepository ImageRepository()
    {
        return _images ??= new ImageRepository(context ?? throw new IncompleteInitialization());
    }

    public ILogementRepository LogementRepository()
    {
        return _logements ??= new LogementRepository(context ?? throw new IncompleteInitialization());
    }

    public IPrestationRepository PrestationRepository()
    {
        return _prestations ??= new PrestationRepository(context ?? throw new IncompleteInitialization());
    }

    public IPrestationProposeeRepository PrestationProposeeRepository()
    {
        return _prestationProposees ??= new PrestationProposeeRepository(context ?? throw new IncompleteInitialization());
    }

    public IRemiseRepository RemiseRepository()
    {
        return _remises ??= new RemiseRepository(context ?? throw new IncompleteInitialization());
    }

    public IReservationRepository ReservationRepository()
    {
        return _reservations ??= new ReservationRepository(context ?? throw new IncompleteInitialization());
    }

    public ITypeDestinationRepository TypeDestinationRepository()
    {
        return _typeDestinations ??= new TypeDestinationRepository(context ?? throw new IncompleteInitialization());
    }

    public IUtilisateurRepository UtilisateurRepository()
    {
        return _utilisateurs ??= new UtilisateurRepository(context ?? throw new IncompleteInitialization());
    }

    public IApplicationUtilisateurRepository ApplicationUtilisateurRepository()
    {
        return _applicationUtilisateurs ??= new ApplicationUtilisateurRepository(
            context ?? throw new IncompleteInitialization(), 
            _userManager ?? throw new IncompleteInitialization(), 
            _roleManager ?? throw new IncompleteInitialization());
    }

    public IApplicationRoleRepository ApplicationRoleRepository()
    {
        return _applicationRoles ??= new ApplicationRoleRepository(
            context ?? throw new IncompleteInitialization(), 
            _roleManager ?? throw new IncompleteInitialization());
    }
    
    public ITransactionRepository TransactionRepository()
    {
        return _transactions ??= new TransactionRepository(context ?? throw new IncompleteInitialization());
    }

    public async Task EnsureDeleteAsync()
    {
        context.Database.EnsureDeleted();
    }

    public async Task EnsureCreatedAsync()
    {
        context.Database.EnsureCreated();
    }

    public async Task SaveChangesAsync()
    {
        context.SaveChangesAsync().Wait();
    }
}