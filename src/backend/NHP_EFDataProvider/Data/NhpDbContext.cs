using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NHP_Domain.Entities;
using NHP_EFDataProvider.Entities;

namespace NHP_EFDataProvider.Data;

public class NhpDbContext : IdentityDbContext<ApplicationUtilisateur, ApplicationRole, string>
{
    private static readonly ILoggerFactory Logger = LoggerFactory.Create(builder => { builder.AddConsole(); });
    
    public NhpDbContext(DbContextOptions<NhpDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseLoggerFactory(Logger)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Table Adresse
        modelBuilder.Entity<Adresse>()
            .HasKey(a => a.Id);
        
        // Table Avis
        modelBuilder.Entity<Avis>()
            .HasKey(a => a.Id);
        
        // Table Image
        modelBuilder.Entity<Image>()
            .HasKey(i => i.Id);
        
        // Table Logement
        modelBuilder.Entity<Logement>()
            .HasKey(l => l.Id);
        
        // Table Prestation
        modelBuilder.Entity<Prestation>()
            .HasKey(p => p.Id);
        
        // OneToMany
        modelBuilder.Entity<Prestation>()
            .HasMany(p => p.PrestationProposees)
            .WithOne(pp => pp.Prestation);
        
        // Table PrestationProposee
        modelBuilder.Entity<PrestationProposee>()
            .HasKey(pp => pp.IdPrestationProposee);
        
        // ManyToOne
        modelBuilder.Entity<PrestationProposee>()
            .HasOne(pp => pp.Prestation)
            .WithMany(p => p.PrestationProposees);
        
        // Table Remise
        modelBuilder.Entity<Remise>()
            .HasKey(r => r.Id);
        
        // OneToMany
        modelBuilder.Entity<Remise>()
            .HasMany(r => r.Reservations)
            .WithOne(res => res.Remise);
        
        // Table Reservation
        modelBuilder.Entity<Reservation>()
            .HasKey(r => r.Id);
        
        // ManyToOne
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Remise)
            .WithMany(r => r.Reservations);
        
        // Table TypeDestination
        modelBuilder.Entity<TypeDestination>()
            .HasKey(td => td.Id);
        
        // Table Utilisateur
        modelBuilder.Entity<Utilisateur>()
            .HasKey(u => u.Id);
        
        // Table ApplicationUtilisateur
        modelBuilder.Entity<ApplicationUtilisateur>()
            .HasOne<Utilisateur>(u => u.Utilisateur)
            .WithOne()
            .HasForeignKey<Utilisateur>();
        modelBuilder.Entity<Utilisateur>()
            .HasOne<ApplicationUtilisateur>()
            .WithOne(u => u.Utilisateur)
            .HasForeignKey<ApplicationUtilisateur>(u => u.UtilisateurId);
        // Inclusion automatique d'Utilisateur dans ApplicationUtilisateur
        modelBuilder.Entity<ApplicationUtilisateur>()
            .Navigation<Utilisateur>(u => u.Utilisateur)
            .AutoInclude();
        
        // Table ApplicationRole
        modelBuilder.Entity<ApplicationRole>()
            .HasOne<Role>(r => r.Role)
            .WithOne()
            .HasForeignKey<Role>();
        modelBuilder.Entity<Role>()
            .HasOne<ApplicationRole>()
            .WithOne(r => r.Role)
            .HasForeignKey<ApplicationRole>(r => r.RoleId);
        modelBuilder.Entity<ApplicationRole>()
            .Navigation<Role>(r => r.Role)
            .AutoInclude();
    }
    
    public DbSet<Adresse> Adresses { get; set; }
    public DbSet<Avis> Avis { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Logement> Logements { get; set; }
    public DbSet<Prestation> Prestations { get; set; }
    public DbSet<PrestationProposee> PrestationProposes { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<TypeDestination> TypeDestinations { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<ApplicationUtilisateur> ApplicationUtilisateurs { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
}