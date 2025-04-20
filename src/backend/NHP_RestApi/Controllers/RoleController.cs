using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Services;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Create;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Delete;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Get;
using NHP_EFDataProvider.Entities;

namespace NHP_RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(
    IRepositoryFactory repositoryFactory, 
    IRoleManagementService roleManagementService,
    IUtilisateurManagementService utilisateurManagementService) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult> GetAllRoles()
    {
        try
        {
            GetAllApplicationRolesUseCase uc = new(repositoryFactory, roleManagementService);
            IEnumerable roles = await uc.ExecuteAsync();
            if (roles == null)
                return NotFound(new { Message = "Aucun rôle trouvé" });
            return Ok(new { Message = "Rôles trouvés", Roles = roles });
        } catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = "Une erreur est survenue lors de la récupération des rôles", Data = ex.Message
            });
        }
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> CreateRole([FromBody] string roleName)
    {
        try
        {
            CreateApplicationRoleUseCase uc = new(repositoryFactory, roleManagementService);
            await uc.ExecuteAsync(roleName);
            return Ok(new { Message = "Rôle créé avec succès" });
        } catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = "Une erreur est survenue lors de la création du rôle", Data = ex.Message
            });
        }
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteRole([FromBody] string roleName)
    {
        try
        {
            DeleteApplicationRoleUseCase uc = new(repositoryFactory, roleManagementService, utilisateurManagementService);
            await uc.ExecuteAsync(roleName);
            return Ok(new { Message = "Rôle supprimé avec succès" });
        } catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = "Une erreur est survenue lors de la suppression du rôle", Data = ex.Message
            });
        }
    }
}