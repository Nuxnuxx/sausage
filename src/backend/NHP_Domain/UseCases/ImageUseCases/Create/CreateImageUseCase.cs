using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ImageUseCases.Create;

public class CreateImageUseCase(IRepositoryFactory factory)
{
    public async Task<Image> ExecuteAsync(String urlImage, String nomImage)
    {
        var image = new Image
        {
            UrlImage = urlImage,
            NomImage = nomImage
        };
        return await ExecuteAsync(image);
    }

    public async Task<Image> ExecuteAsync(Image image)
    {
        await CheckBusinessRules(image);
        Image im = await factory.ImageRepository().CreateAsync(image);
        factory.ImageRepository().SaveChangesAsync().Wait();
        return im;
    }

    private async Task CheckBusinessRules(Image image)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentNullException.ThrowIfNull(image.NomImage);
        ArgumentNullException.ThrowIfNull(image.UrlImage);
        ArgumentNullException.ThrowIfNull(factory.ImageRepository());
    }
}