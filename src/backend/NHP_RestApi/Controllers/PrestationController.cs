using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.PrestationUseCases.Create;
using NHP_Domain.UseCases.PrestationUseCases.Delete;
using NHP_Domain.UseCases.PrestationUseCases.Get;
using NHP_Domain.UseCases.PrestationUseCases.Update;

namespace NHP_RestApi.Controllers;

[Route("api/prestation")]
[ApiController]
public class PrestationController (IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<PrestationDto>>> GetAllAsync()
    {
        GetTouteLesPrestationsUseCase uc = new (repositoryFactory);
        List<PrestationDto> prestationDtos = new();
        
        List<Prestation> prestations = await uc.ExecuteAsync();
        foreach (var prestation in prestations)
        {
            prestationDtos.Add(new PrestationDto()
            {
                Id = prestation.Id,
                NomPrestation = prestation.NomPrestation
            });
        }
        return prestationDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrestationDto>> GetUnePrestationAsync(long id)
    {
        GetPrestationByPrimaryKeyUseCase uc = new (repositoryFactory);
        PrestationDto prestationDto = new();

        Prestation prestation = await uc.ExecuteAsync(id);
        prestationDto.Id = prestation.Id;
        prestationDto.NomPrestation = prestation.NomPrestation;
        
        return prestationDto;
    }

    [HttpPost]
    public async Task<ActionResult<PrestationDto>> PostAsync([FromBody] PrestationDto prestationDto)
    {
        CreatePrestationUseCase uc = new (repositoryFactory);
        Prestation prestation = prestationDto.ToEntity();

        try
        {
            await uc.ExecuteAsync(prestation);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }

        return CreatedAtAction(nameof(GetUnePrestationAsync), new { id = prestation.Id }, new PrestationDto().ToDto(prestation));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(long id, [FromBody] PrestationDto prestationDto)
    {
        if (id != prestationDto.Id)
        {
            return BadRequest();
        }
        
        UpdatePrestationUseCase uc = new (repositoryFactory);
        Prestation prestation = prestationDto.ToEntity();

        try
        {
            await uc.ExecuteAsync(prestation);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        DeletePrestationUseCase uc = new (repositoryFactory);
        try
        {
            uc.ExecuteAsync(id);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return NoContent();
    }
}