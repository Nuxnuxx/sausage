using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.TypeDestinationUseCases.Create;
using NHP_Domain.UseCases.TypeDestinationUseCases.Delete;
using NHP_Domain.UseCases.TypeDestinationUseCases.Get;
using NHP_Domain.UseCases.TypeDestinationUseCases.Update;

namespace NHP_RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeDestinationController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<TypeDestinationDto>>> GetAsync()
    {
        GetAllTypeDestinationUseCase uc = new(repositoryFactory);

        var typeDestinations = await uc.ExecuteAsync();
        return typeDestinations.Select(typeDestination => new TypeDestinationDto() { NomTypeDestination = typeDestination.NomTypeDestination, DescriptionTypeDestination = typeDestination.DescriptionTypeDestination }).ToList();
    }

    [HttpGet("{nomTypeDestination}")]
    public async Task<ActionResult<TypeDestinationDto>> GetUneTypeDestination(string nomTypeDestination)
    {
        GetTypeDestinationByPrimaryKeyUseCase uc = new(repositoryFactory);
        TypeDestinationDto typeDestinationDto = new();
        
        var typeDestination = await uc.ExecuteAsync(nomTypeDestination);
        
        if (typeDestination == null)
            return NotFound();

        typeDestinationDto.NomTypeDestination = typeDestination.NomTypeDestination;
        typeDestinationDto.DescriptionTypeDestination = typeDestination.DescriptionTypeDestination;
        
        return typeDestinationDto;
    }

    [HttpPost]
    public async Task<ActionResult<TypeDestinationDto>> PostAsync([FromBody] TypeDestinationDto typeDestinationDto)
    {
        CreateTypeDestinationUseCase uc = new(repositoryFactory);
        TypeDestination typeDestination = typeDestinationDto.ToEntity();
        try
        {
            await uc.ExecuteAsync(typeDestination);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        } 
        return CreatedAtAction(nameof(GetUneTypeDestination), 
            new { nomTypeDestination = typeDestination.NomTypeDestination }, 
            new TypeDestinationDto().ToDto(typeDestination));
    }

    [HttpPut("{nomTypeDestination}")]
    public async Task<IActionResult> PutAsync(string nomTypeDestination, [FromBody] TypeDestinationDto typeDestinationDto)
    {
        if (nomTypeDestination != typeDestinationDto.NomTypeDestination)
        {
            return BadRequest();
        }
        
        UpdateTypeDestinationUseCase uc = new(repositoryFactory);
        TypeDestination typeDestination = typeDestinationDto.ToEntity();

        try
        {
            await uc.ExecuteAsync(typeDestination);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        
        return NoContent();
    }

    [HttpDelete("{nomTypeDestination}")]
    public async Task<IActionResult> DeleteAsync(string nomTypeDestination)
    {
        DeleteTypeDestinationUseCase uc = new(repositoryFactory);
        try
        {
            var deleted = await uc.ExecuteAsync(nomTypeDestination);
            if (!deleted)
                return NotFound();
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return NoContent();
    }
}