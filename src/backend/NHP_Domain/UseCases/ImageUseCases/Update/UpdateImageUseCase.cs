using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ImageUseCases.Update;

public class UpdateImageUseCase(IRepositoryFactory factory)
{
    public async Task<Image?> ExecuteAsync(Image image)
    {
        await CheckBusinessRules(image);
        await factory.ImageRepository().UpdateAsync(image);
        await factory.ImageRepository().SaveChangesAsync();
        return image;
    }

    private async Task CheckBusinessRules(Image image)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentNullException.ThrowIfNull(image.UrlImage);
        ArgumentNullException.ThrowIfNull(image.NomImage);
        ArgumentNullException.ThrowIfNull(factory);
    }
}