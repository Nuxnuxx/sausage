using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.AvisUseCases.Create;
using NHP_Domain.UseCases.ImageUseCases.Create;
using NHP_Domain.UseCases.ImageUseCases.Delete;
using NHP_Domain.UseCases.ImageUseCases.Get;
using NHP_Domain.UseCases.ImageUseCases.Update;

namespace NHP_RestApi.Controllers;

[Route("api/image")]
[ApiController]
public class ImageController (IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<ImageDto>>> GetAsync()
    {
        GetTouteLesImagesUseCases uc = new (repositoryFactory);
        List<ImageDto> imageDto = new();
        
        List<Image> images = await uc.ExecuteAsync();
        foreach (var image in images)
        {
            imageDto.Add(new ImageDto()
            {
                UrlImage = image.UrlImage,
                NomImage = image.NomImage,
            });
        }
        return imageDto;
        
    }

    [HttpGet("{urlImage}")]
    public async Task<ActionResult<ImageDto>> GetUneImage(string urlImage)
    {
        GetImageByPrimaryKeyUseCase uc = new (repositoryFactory);
        ImageDto imageDto = new();
        
        Image image = await uc.ExecuteAsync(urlImage);
        
        imageDto.UrlImage = image.UrlImage;
        imageDto.NomImage = image.NomImage;
        
        return imageDto;
    }

    [HttpPost]
    public async Task<ActionResult<ImageDto>> PostAsync([FromBody] ImageDto imageDto)
    {
        CreateImageUseCase uc = new (repositoryFactory);
        Image image = imageDto.ToEntity();
        try
        {
            await uc.ExecuteAsync(image);

        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        } 
        return CreatedAtAction(nameof(GetUneImage), new { urlImage = image.UrlImage }, new ImageDto().ToDto(image));
    }

    [HttpPut("{urlImage}")]
    public async Task<IActionResult> PutAsync(string urlImage, [FromBody] ImageDto imageDto)
    {
        if (urlImage != imageDto.UrlImage)
        {
            return BadRequest();
        }
        
        UpdateImageUseCase uc = new (repositoryFactory);
        Image image = imageDto.ToEntity();

        try
        {
            await uc.ExecuteAsync(image);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        
        return NoContent();
    }

    [HttpDelete("{urlImage}")]
    public async Task<IActionResult> DeleteAsync(string urlImage)
    {
        DeleteImageUseCase uc = new (repositoryFactory);
        try
        {
            await uc.ExecuteAsync(urlImage);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return NoContent();
    }
    
}

