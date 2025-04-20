using NHP_Domain.DataAdapters;
using NHP_Domain.Entities;
using NHP_EFDataProvider.Data;

namespace NHP_EFDataProvider.Repositories;

public class PrestationProposeeRepository(NhpDbContext context) : Repository<PrestationProposee>(context), 
    IPrestationProposeeRepository
{

}