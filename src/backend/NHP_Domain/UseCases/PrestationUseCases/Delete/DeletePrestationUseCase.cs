using System.Xml.Schema;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.PrestationUseCases.Delete;

public class DeletePrestationUseCase(IRepositoryFactory factory)
{
    public async Task<bool> ExecuteAsync(Prestation prestation)
    {
        await CheckBusinessRules();
        await factory.PrestationRepository().DeleteAsync(prestation);
        return true;
    }

    public async Task<bool> ExecuteAsync(long id)
    {
        await CheckBusinessRules();
       
        var prestation = await factory.PrestationRepository()
            .FindByConditionAsync(a => a.Id == id);

        if (!prestation.Any())
        {
            return false;
        }
        
        await factory.PrestationRepository().DeleteAsync(prestation.First());

        return true;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IPrestationRepository prestationRepository = factory.PrestationRepository();
        ArgumentNullException.ThrowIfNull(prestationRepository);
    }
}