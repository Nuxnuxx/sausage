using Microsoft.EntityFrameworkCore;
using NHP_Domain.DataAdapters;
using NHP_Domain.Entities;
using NHP_EFDataProvider.Data;

namespace NHP_EFDataProvider.Repositories;

public class UtilisateurRepository(NhpDbContext context) : Repository<Utilisateur>(context), IUtilisateurRepository
{
    public async Task<Utilisateur?> GetByEmailAsync(string email)
    {
        return await context.Utilisateurs.FirstAsync(e => e.MailUtilisateur == email);
    }
}