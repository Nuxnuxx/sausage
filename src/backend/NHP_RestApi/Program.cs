using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.DataSets;
using NHP_Domain.Services;
using NHP_EFDataProvider.Data;
using NHP_EFDataProvider.Entities;
using NHP_EFDataProvider.RepositoryFactories;
using NHP_EFDataProvider.Services;
using NHP_RestApi.Configuration;

// Chargement des variables d'environnement
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données
var connectionString = DatabaseConfiguration.GetConnectionString();
Console.WriteLine($"Tentative de connexion à la base de données : {connectionString}");

builder.Services.AddDbContext<NhpDbContext>(options => 
    DatabaseConfiguration.ConfigureDbContext(options, connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // URL frontend
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(options =>
{
    options.ClearProviders();
    options.AddConsole();
});

builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddScoped<UserManager<ApplicationUtilisateur>>();

builder.Services.AddScoped<IUtilisateurManagementService, UtilisateurManagementService>();
builder.Services.AddScoped<IRoleManagementService, RoleManagementService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Configuration de l'authentification JWT et cookies
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MIAGE_PLUS_estlavraieassociationmiage")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
})
.AddCookie(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/api/Auth/login";
    options.LogoutPath = "/api/Auth/logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<ApplicationUtilisateur>()
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<NhpDbContext>()
    .AddApiEndpoints();
    
var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

// Cette ligne permet d'ajouter les routes par défaut pour l'authentification, la connexion et la déconnexion
// On va ici la désactiver pour ne pas avoir de conflit avec l'API d'authentification que l'on va créer
//app.MapIdentityApi<ApplicationUtilisateur>();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<NhpDbContext>>();
    DbContext context = scope.ServiceProvider.GetRequiredService<NhpDbContext>();
    logger.LogInformation("Initialisation de la base de données");
    logger.LogInformation("Suppression de la BD si elle existe");
    await context.Database.EnsureDeletedAsync();
    logger.LogInformation("Création de la BD et des tables à partir des entities");
    await context.Database.EnsureCreatedAsync();
    
    /* INIT SIMULATION DES DONNEES */
    IRepositoryFactory repositoryFactory = scope.ServiceProvider.GetRequiredService<IRepositoryFactory>();  
    IRoleManagementService roleManagement = scope.ServiceProvider.GetRequiredService<IRoleManagementService>();  
    
    BdBuilder seedBd = new BasicBdBuilder(repositoryFactory, roleManagement);
    await seedBd.BuildDbAsync();
    /* FIN INIT SIMULATION DES DONNEES */
}

await app.RunAsync();
