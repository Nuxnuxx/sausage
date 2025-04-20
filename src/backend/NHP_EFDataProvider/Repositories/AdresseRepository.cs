using NHP_Domain.DataAdapters;
using NHP_Domain.Entities;
using NHP_EFDataProvider.Data;

namespace NHP_EFDataProvider.Repositories;

public class AdresseRepository(NhpDbContext context) : Repository<Adresse>(context), IAdresseRepository
{
    
}