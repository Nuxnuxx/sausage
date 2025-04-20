using Microsoft.EntityFrameworkCore;
using NHP_Domain.DataAdapters;
using NHP_Domain.Entities;
using NHP_EFDataProvider.Data;

namespace NHP_EFDataProvider.Repositories;

public class AvisRepository(NhpDbContext context) : Repository<Avis>(context), IAvisRepository
{
    
}