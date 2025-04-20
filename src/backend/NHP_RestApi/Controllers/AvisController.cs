using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.AvisUseCases.Create;
using NHP_Domain.UseCases.AvisUseCases.Delete;
using NHP_Domain.UseCases.AvisUseCases.Get;
using NHP_Domain.UseCases.AvisUseCases.Update;

namespace NHP_RestApi.Controllers;

[Route("apis/avis")]
[ApiController]
public class AvisController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAllFromLogement")]
    public async Task<ActionResult<List<AvisDto>>> GetAllFromLogement([FromBody]string nomLogement)
    {
        GetAvisFromLogementUseCase uc = new (repositoryFactory);
        List<AvisDto> avisDtos = new();
        
        List<Avis> avisList = await uc.ExecuteAsync(nomLogement);
        foreach (var avis in avisList)
        {
            avisDtos.Add(new AvisDto()
            {
                Id = avis.Id,
                NoteAvis = avis.NoteAvis,
                TexteAvis = avis.TexteAvis,
            });
        }
        
        return avisDtos;
    }

    [HttpGet("getAllFromUser")]
    public async Task<ActionResult<List<AvisDto>>> GetAllFromUser([FromBody] string nomUser)
    {
        GetAvisFromUserUseCase uc = new (repositoryFactory);
        List<AvisDto> avisDtos = new();
        
        List<Avis> avisList = await uc.ExecuteAsync(nomUser);
        foreach (var avis in avisList)
        {
            avisDtos.Add(new AvisDto()
            {
                Id = avis.Id,
                NoteAvis = avis.NoteAvis,
                TexteAvis = avis.TexteAvis,
            });
        }
        
        return avisDtos;
    }
    
    [HttpGet("getRandom/{number}")]
    public async Task<ActionResult<List<AvisDto>>> GetRandom([FromRoute]int number)
    {
        GetRandomAvisUseCase uc = new (repositoryFactory);
        List<AvisDto> avisDtos = new();
        
        List<Avis> avisList = await uc.ExecuteAsync(number);
        foreach (var avis in avisList)
        {
            avisDtos.Add(new AvisDto()
            {
                Id = avis.Id,
                NoteAvis = avis.NoteAvis,
                TexteAvis = avis.TexteAvis,
            });
        }
        
        return avisDtos;
    }

    [HttpPost]
    public async Task<ActionResult<AvisDto>> PostAsync([FromBody] AvisDto avisDto)
    {
        CreateAvisUseCase uc = new (repositoryFactory);
        Avis avis = avisDto.ToEntity();
        try
        {
            await uc.ExecuteAsync(avis);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return CreatedAtAction(nameof(GetAllFromLogement), new {Id = avis.Id}, new AvisDto().ToDto(avis) );
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> PutAsync(long Id, [FromBody] AvisDto avisDto)
    {
        if (Id != avisDto.Id)
        {
            return BadRequest();
        }
        
        UpdateAvisUseCase uc = new (repositoryFactory);
        Avis avis = avisDto.ToEntity();

        try
        {
            uc.ExecuteAsync(avis);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        
        return NoContent();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAsync(string mailUtilisateur, string nomLogement)
    {
        DeleteAvisUseCase uc = new (repositoryFactory);
        try
        {
            uc.ExecuteAsync(mailUtilisateur, nomLogement);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return NoContent();
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopAvis([FromQuery] int nombre = 5)
    {
        var useCase = new GetTopAvisUseCase(repositoryFactory);
        var avis = await useCase.ExecuteAsync(nombre);
        return Ok(avis);
    }
}