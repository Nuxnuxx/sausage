using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ImageUseCases.Get;

public class GetTouteLesImagesUseCases(IRepositoryFactory factory)
{
    public async Task<List<Image>> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Image> images = await factory.ImageRepository().FindAllAsync();
        return images;
    }

    private async Task CheckBusinessRules() 
    {
        ArgumentNullException.ThrowIfNull(factory);
        IImageRepository imageRepository = factory.ImageRepository();
        ArgumentNullException.ThrowIfNull(imageRepository);
    }
}