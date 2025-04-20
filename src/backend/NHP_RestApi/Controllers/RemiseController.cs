using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.RemiseUseCases.Create;
using NHP_Domain.UseCases.RemiseUseCases.Delete;
using NHP_Domain.UseCases.RemiseUseCases.Get;
using NHP_Domain.UseCases.RemiseUseCases.Update;

namespace NHP_RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RemiseController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<Remise>>> GetAsync()
    {
        GetAllRemisesUseCase uc = new(repositoryFactory);
        var remises = await uc.ExecuteAsync();
        return remises;
    }

    [HttpGet("{codeRemise}")]
    public async Task<ActionResult<Remise>> GetUneRemise(string codeRemise)
    {
        GetRemiseByCodeUseCase uc = new(repositoryFactory);
        var remise = await uc.ExecuteAsync(codeRemise);
        
        if (remise == null)
            return NotFound(new { Message = "Remise non trouvée" });
        
        return remise;
    }

    [HttpPost]
    public async Task<ActionResult<Remise>> PostAsync([FromBody] Remise remise)
    {
        CreateRemiseUseCase uc = new(repositoryFactory);
        try
        {
            var createdRemise = await uc.ExecuteAsync(remise);
            return CreatedAtAction(nameof(GetUneRemise), new { codeRemise = createdRemise.CodeRemise }, createdRemise);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = "Erreur lors de la création de la remise", Error = e.Message });
        }
    }

    [HttpPut("{codeRemise}")]
    public async Task<IActionResult> PutAsync(string codeRemise, [FromBody] Remise remise)
    {
        if (codeRemise != remise.CodeRemise)
        {
            return BadRequest(new { Message = "Le code remise ne correspond pas" });
        }
        
        UpdateRemiseUseCase uc = new(repositoryFactory);

        try
        {
            await uc.ExecuteAsync(remise);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = "Erreur lors de la mise à jour de la remise", Error = e.Message });
        }
    }

    [HttpDelete("{codeRemise}")]
    public async Task<IActionResult> DeleteAsync(string codeRemise)
    {
        DeleteRemiseUseCase uc = new(repositoryFactory);
        try
        {
            var deleted = await uc.ExecuteAsync(codeRemise);
            
            if (deleted)
                return NoContent();
            
            return NotFound(new { Message = "Remise non trouvée" });
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = "Erreur lors de la suppression de la remise", Error = e.Message });
        }
    }
}