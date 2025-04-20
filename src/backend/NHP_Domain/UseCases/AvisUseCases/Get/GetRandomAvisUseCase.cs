using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.AvisUseCases.Get;

public class GetRandomAvisUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<List<Avis>> ExecuteAsync(int number)
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.AvisRepository());

        List<Avis> avis = await repositoryFactory.AvisRepository().FindAllAsync();
        Random random = new();
        List<Avis> randomAvis = new();
        
        if(avis.Count < number) 
        {
            number = avis.Count;
        }

        for (int i = 0; i < number; i++)
        {
            int index = random.Next(avis.Count);
            randomAvis.Add(avis[index]);
            avis.RemoveAt(index);
        }

        return randomAvis;
        
    }
}