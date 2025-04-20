    using NHP_Domain.DataAdapters;
    using NHP_Domain.DataAdapters.DataAdaptersFactory;
    using NHP_Domain.Entities;

    namespace NHP_Domain.UseCases.ImageUseCases.Get;

    public class GetImageByPrimaryKeyUseCase(IRepositoryFactory factory)
    {
        public async Task<Image?> ExecuteAsync(string urlImage)
        {
            await CheckBusinessRules();
            return await factory.ImageRepository().FindAsync(urlImage);
        }

        private async Task CheckBusinessRules()
        {
            ArgumentNullException.ThrowIfNull(factory);
            IImageRepository imageRepository = factory.ImageRepository();
            ArgumentNullException.ThrowIfNull(imageRepository);
        }
    }