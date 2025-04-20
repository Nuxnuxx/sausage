using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NHP_Domain.DataAdapters;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.UtilisateurExceptions;
using NHP_EFDataProvider.Data;
using NHP_EFDataProvider.Entities;

namespace NHP_EFDataProvider.Repositories;

public class ApplicationUtilisateurRepository(
    NhpDbContext context, 
    UserManager<ApplicationUtilisateur> userManager,
    RoleManager<ApplicationRole> roleManager)
    : Repository<IApplicationUtilisateur>(context), IApplicationUtilisateurRepository
{
    public async Task<IApplicationUtilisateur?> AddUserAsync(string login, string email, string password, string role, Utilisateur user)
    {
        ApplicationUtilisateur utilisateur = new ApplicationUtilisateur
        {
            UserName = login,
            Email = email,
            Utilisateur = user
        };
        IdentityResult result = await userManager.CreateAsync(utilisateur, password);
        
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(utilisateur, role);
        }
        await Context.SaveChangesAsync();
        return result.Succeeded ? utilisateur : null;
    }

    public async Task<IApplicationUtilisateur?> FindByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }
    
    public async Task<IApplicationUtilisateur?> FindByIdAsync(string id)
    {
        return await userManager.FindByIdAsync(id);
    }

    public async Task<IEnumerable<IApplicationUtilisateur>> GetAllAsync()
    {
        return await Context.ApplicationUtilisateurs
            .Include(u => u.Utilisateur)
            .ToListAsync();
    }

    public async Task<bool> IsInRoleAsync(string email, string role)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
            return false;
        
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return false;
        
        return await userManager.IsInRoleAsync(user, role);
    }

    public async Task UpdateAsync(Utilisateur user)
    {
        ApplicationUtilisateur? utilisateur = await Context
            .ApplicationUtilisateurs.Include(u => u.Utilisateur)
            .FirstOrDefaultAsync(u => u.UtilisateurId == user.Id);
        if (utilisateur == null) return;
        if (utilisateur.Utilisateur == null) return;
        
        // Mise à jour des valeurs sans attacher une nouvelle instance
        utilisateur.Utilisateur.NomUtilisateur = user.NomUtilisateur;
        utilisateur.Utilisateur.PrenomUtilisateur = user.PrenomUtilisateur;
        utilisateur.Utilisateur.MailUtilisateur = user.MailUtilisateur;
        utilisateur.Utilisateur.Adresse = user.Adresse;
        utilisateur.Utilisateur.TelephoneUtilisateur = user.TelephoneUtilisateur;
        utilisateur.Utilisateur.NewsLetterUtilisateur = user.NewsLetterUtilisateur;
        
        await Context.SaveChangesAsync();
    }
    
    public async Task<bool> DeleteAsync(string email)
    {
        ApplicationUtilisateur? utilisateur = await userManager.FindByEmailAsync(email);
        if (utilisateur == null) return false;
        
        IdentityResult result = await userManager.DeleteAsync(utilisateur);
        await Context.SaveChangesAsync();
        return result.Succeeded;
    }
}