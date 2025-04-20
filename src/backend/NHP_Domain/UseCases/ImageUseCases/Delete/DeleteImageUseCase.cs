using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ImageUseCases.Delete;

public class DeleteImageUseCase(IRepositoryFactory factory)
{
    public async Task<bool> ExecuteAsync(string urlImage)
    {
        await CheckBusinessRules();
       
        var image = await factory.ImageRepository()
            .FindByConditionAsync(a => a.UrlImage == urlImage);

        if (!image.Any())
        {
            return false;
        }
        
        await factory.ImageRepository().DeleteAsync(image.First());

        return true;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(factory);
        IImageRepository imageRepository = factory.ImageRepository();
        ArgumentNullException.ThrowIfNull(imageRepository);
        
    }
}