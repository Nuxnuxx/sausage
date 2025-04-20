using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Delete;

public class DeletePrestationProposeeUseCase(IRepositoryFactory factory)
{
    public async Task<bool> ExecuteAsync(PrestationProposee prestationProposee)
    {
        await CheckBusinessRules();
        await factory.PrestationProposeeRepository().DeleteAsync(prestationProposee);

        return true;
    }

    public async Task<bool> ExecuteAsync(long id)
    {
        await CheckBusinessRules();
       
        var prestationProposees = await factory.PrestationProposeeRepository()
            .FindByConditionAsync(a => a.IdPrestationProposee == id);

        if (!prestationProposees.Any())
        {
            return false;
        }
        
        await factory.PrestationProposeeRepository().DeleteAsync(prestationProposees.First());

        return true;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IPrestationProposeeRepository prestationProposeeRepository = factory.PrestationProposeeRepository();
        ArgumentNullException.ThrowIfNull(prestationProposeeRepository);
    }
}