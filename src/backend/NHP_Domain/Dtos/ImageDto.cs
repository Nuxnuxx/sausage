using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class ImageDto
{
    public String UrlImage { get; set; }
    public String NomImage { get; set; }

    public ImageDto ToDto(Image image)
    {
        this.UrlImage = image.UrlImage;
        this.NomImage = image.NomImage;
        return this;
    }

    public List<ImageDto> ToDtos(List<Image> images)
    {
        return images.Select(i => new ImageDto().ToDto(i)).ToList();
    }

    public Image ToEntity()
    {
        return new Image
        {
            UrlImage = this.UrlImage,
            NomImage = this.NomImage,
        };
    }
}