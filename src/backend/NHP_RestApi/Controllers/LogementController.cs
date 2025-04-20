using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.ImageUseCases.Get;
using NHP_Domain.UseCases.LogementUseCases.Create;
using NHP_Domain.UseCases.LogementUseCases.Delete;
using NHP_Domain.UseCases.LogementUseCases.Get;
using NHP_Domain.UseCases.LogementUseCases.Update;
using NHP_EFDataProvider.RepositoryFactories;

namespace NHP_RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogementController (IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<LogementDto>>> GetAsync([FromQuery] string? nomLogement = null, [FromQuery] string? destinationNom = null)
    {
        GetAllLogementsUseCase uc = new (repositoryFactory);
        List<LogementDto> logementsDtos = new();
        
        try
        {
            var logements = await uc.ExecuteAsync(nomLogement, destinationNom);
            logementsDtos = new LogementDto().ToDtos(logements);
            return Ok(logementsDtos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<LogementDto>> GetUnLogement(long Id)
    {
        GetLogementUseCase uc = new (repositoryFactory);
        
        try
        {
            var logement = await uc.ExecuteAsync(Id);
            var logementDto = new LogementDto().ToDto(logement);
            return Ok(logementDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<LogementDto>> PostAsync([FromBody] LogementDto logementDto)
    {
        CreateLogementUseCase uc = new (repositoryFactory);
        Logement logement = logementDto.ToEntity();
        try
        {
            var createdLogement = await uc.ExecuteAsync(logement);
            var createdLogementDto = new LogementDto().ToDto(createdLogement);
            return CreatedAtAction(nameof(GetUnLogement), new { Id = createdLogementDto.Id }, createdLogementDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult<LogementDto>> PutAsync(long Id, [FromBody] LogementDto logementDto)
    {
        if (Id != logementDto.Id)
        {
            return BadRequest("L'ID dans l'URL ne correspond pas à l'ID dans le corps de la requête");
        }
        
        UpdateLogementUseCase uc = new (repositoryFactory);
        Logement logement = logementDto.ToEntity();

        try
        {
            var updatedLogement = await uc.ExecuteAsync(logement);
            var updatedLogementDto = new LogementDto().ToDto(updatedLogement);
            return Ok(updatedLogementDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAsync(long Id)
    {
        DeleteLogementUseCase uc = new (repositoryFactory);
        try
        {
            await uc.ExecuteAsync(Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("by-destination/{typeDestinationId}")]
    public async Task<IActionResult> GetLogementsByTypeDestination(long typeDestinationId)
    {
        var useCase = new GetLogementsByTypeDestinationUseCase(repositoryFactory);
        var logements = await useCase.ExecuteAsync(typeDestinationId);
        return Ok(logements);
    }

    [HttpGet("top-rated")]
    public async Task<IActionResult> GetTopRatedLogements([FromQuery] int nombre = 5)
    {
        var useCase = new GetTopRatedLogementsUseCase(repositoryFactory);
        var logements = await useCase.ExecuteAsync(nombre);
        return Ok(logements);
    }

    [HttpGet("top-rated-by-destination/{destinationId}")]
    public async Task<IActionResult> GetTopRatedLogementsByDestination(long destinationId, [FromQuery] int nombre = 5)
    {
        var useCase = new GetTopRatedLogementsByDestinationUseCase(repositoryFactory);
        var logements = await useCase.ExecuteAsync(destinationId, nombre);
        return Ok(logements);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomLogements([FromQuery] int nombre = 5)
    {
        var useCase = new GetRandomLogementsUseCase(repositoryFactory);
        var logements = await useCase.ExecuteAsync(nombre);
        return Ok(logements);
    }
}