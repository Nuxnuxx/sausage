using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.Services;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Create;

namespace NHP_RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IRepositoryFactory repositoryFactory, 
    IAuthenticationService authenticationService, 
    IUtilisateurManagementService utilisateurManagementService,
    IRoleManagementService roleManagementService,
    ITokenService tokenService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] InscriptionDto request)
    {
        CreateUtilisateurWithRoleUseCase uc = new CreateUtilisateurWithRoleUseCase(repositoryFactory, 
            utilisateurManagementService, roleManagementService);
        var (user, success, errors) = await uc.ExecuteAsync(
            request.Email,
            request.Password,
            request.Nom,
            request.Prenom,
            "Client",
            request.Civilite,
            request.Telephone,
            request.Newsletter);
        
        if (success)
        {
            return Ok(new { Message= "Utilisateur inscrit avec success !", User = user });
        }
        return BadRequest(new { Message = "Une erreur est survenue lors de l'inscription", Data = errors });
        
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ConnexionDto request)
    {
        var (succeeded, userId) = await authenticationService.SignInAsync(request.Email, request.Password);
        if (succeeded)
        {
            var role = await utilisateurManagementService.GetUserRolesAsync(userId);
            if(role.Count == 0)
                return BadRequest(new { Message = "Aucun role trouvé pour cet utilisateur" });
            var token = tokenService.GenerateTokenAsync(userId, request.Email, role[0]);
            var user = await repositoryFactory.ApplicationUtilisateurRepository().FindByIdAsync(userId);
            if (user == null || user.Utilisateur == null)
                return BadRequest(new { Message = "Utilisateur introuvable" });
            return Ok(new { Message= "Utilisateur connecté avec success !", User = user.Utilisateur, Role = role, Token = token });
        }
        return BadRequest(new { Message = "Une erreur est survenue lors de la connexion", 
            Data = "Email ou mot de passe incorrect" });
    }
    

}