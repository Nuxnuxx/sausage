using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NHP_Domain.DataAdapters;
using NHP_Domain.Entities;
using NHP_EFDataProvider.Data;

namespace NHP_EFDataProvider.Repositories;

public class LogementRepository(NhpDbContext context) : Repository<Logement>(context), ILogementRepository
{
    public override async Task<List<Logement>> FindByConditionAsync(Expression<Func<Logement, bool>> condition)
    {
        // Include related entities to enable filtering on their properties
        return await Context.Set<Logement>()
            .Include(l => l.Destination)
            .Include(l => l.Image)
            .Include(l => l.Adresse)
            .Where(condition)
            .ToListAsync();
    }
    
    public override async Task<List<Logement>> FindAllAsync()
    {
        // Include related entities 
        return await Context.Set<Logement>()
            .Include(l => l.Destination)
            .Include(l => l.Image)
            .Include(l => l.Adresse)
            .ToListAsync();
    }
}