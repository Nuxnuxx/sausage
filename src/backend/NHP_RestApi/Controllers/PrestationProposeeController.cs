using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.PrestationUseCases.Create;
using NHP_Domain.UseCases.PrestationUseCases.Delete;
using NHP_Domain.UseCases.PrestationUseCases.Get;
using NHP_Domain.UseCases.PrestationUseCases.Update;

namespace NHP_RestApi.Controllers;

[Route("api/prestationproposee")]
[ApiController]
public class PrestationProposeeController (IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<PrestationProposeeDto>>> GetAllAsync()
    {
        GetTouteLesPrestationsProposeeUseCase uc = new(repositoryFactory);
        List<PrestationProposeeDto> prestationProposeeDtos = new();
        
        List<PrestationProposee> prestationProposees = await uc.ExecuteAsync();
        foreach (var prestationProposee in prestationProposees)
        {
            prestationProposeeDtos.Add(new PrestationProposeeDto()
            {
                IdPrestationProposee = prestationProposee.IdPrestationProposee,
                PrixPrestation = prestationProposee.PrixPrestation
            });
        }
        return prestationProposeeDtos;
    }

    [HttpGet("{idPrestationProposee}")]
    public async Task<ActionResult<PrestationProposeeDto>> GetUnePrestationProposeeAsync(long idPrestationProposee)
    {
        GetPrestationProposeeByPrimaryKeyUseCase uc = new (repositoryFactory);
        PrestationProposeeDto prestationProposeeDto = new();

        PrestationProposee prestationProposee = await uc.ExecuteAsync(idPrestationProposee);
        prestationProposeeDto.IdPrestationProposee = prestationProposee.IdPrestationProposee;
        prestationProposeeDto.PrixPrestation = prestationProposee.PrixPrestation;
        
        return prestationProposeeDto;
    }

    [HttpPost]
    public async Task<ActionResult<PrestationProposeeDto>> PostAsync([FromBody] PrestationProposeeDto prestationProposeeDto)
    {
        CreatePrestationProposeeUseCase uc = new (repositoryFactory);
        PrestationProposee prestationProposee = prestationProposeeDto.ToEntity();

        try
        {
            uc.ExecuteAsync(prestationProposee);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        
        return CreatedAtAction(nameof(GetUnePrestationProposeeAsync), new {id = prestationProposee.IdPrestationProposee}, new PrestationProposeeDto().ToDto(prestationProposee));
    }

    [HttpPut("{idPrestationProposee}")]
    public async Task<IActionResult> PutAsync(long id, [FromBody] PrestationProposeeDto prestationProposeeDto)
    {
        if (id != prestationProposeeDto.IdPrestationProposee)
        {
            return BadRequest();
        }

        UpdatePrestationProposeeUseCase uc = new(repositoryFactory);
        PrestationProposee prestationProposee = prestationProposeeDto.ToEntity();

        try
        {
            await uc.ExecuteAsync(prestationProposee);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return NoContent();
    }

    [HttpDelete("{idPrestationProposee}")]
    public async Task<IActionResult> DeleteAsync(long idPrestationProposee)
    {
        DeletePrestationProposeeUseCase uc = new(repositoryFactory);
        try
        {
            uc.ExecuteAsync(idPrestationProposee);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return NoContent();
    }
}