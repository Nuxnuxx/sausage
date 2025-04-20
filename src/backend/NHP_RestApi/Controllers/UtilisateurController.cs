using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.Services;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Delete;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Get;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Update;
using NHP_Domain.UseCases.UtilisateurUseCases.Get;

namespace NHP_RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilisateurController(
    IRepositoryFactory repositoryFactory,
    IUtilisateurManagementService utilisateurManagementService) : ControllerBase
{
    
    [HttpGet("getAll")]
    public async Task<ActionResult<List<Utilisateur>>> GetAsync()
    {
        GetAllApplicationUtilisateursUseCase uc = new (repositoryFactory);
        IEnumerable<IApplicationUtilisateur> applicationUtilisateur = await uc.ExecuteAsync();
        if (applicationUtilisateur == null)
            return NotFound(new { Message = "Aucun utilisateur trouvé" });
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        foreach (IApplicationUtilisateur user in applicationUtilisateur)
        {
            utilisateurs.Add(new Utilisateur
            {
                Id = user.Utilisateur.Id,
                NomUtilisateur = user.Utilisateur.NomUtilisateur,
                PrenomUtilisateur = user.Utilisateur.PrenomUtilisateur,
                MailUtilisateur = user.Utilisateur.MailUtilisateur,
                CiviliteUtilisateur = user.Utilisateur.CiviliteUtilisateur,
                TelephoneUtilisateur = user.Utilisateur.TelephoneUtilisateur,
                NewsLetterUtilisateur = user.Utilisateur.NewsLetterUtilisateur
            });
        }
        return utilisateurs;
    }
    
    [HttpGet("{email}")]
    public async Task<IActionResult> GetUnUtilisateur(string email)
    {
        try
        {
            GetApplicationUtilisateurUseCase uc = new(repositoryFactory);
            IApplicationUtilisateur applicationUtilisateur = await uc.ExecuteAsync(email);
            return Ok(new { Message = "Utilisateur trouvé", Utilisateur = applicationUtilisateur });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = "Une erreur est survenue lors de la récupération de l'utilisateur" , Data = ex.Message
            });
        }
    }
    
    [HttpGet("id/{id}")]
    public async Task<ActionResult<UtilisateurDto>> GetUtilisateur(string id)
    {
        var useCase = new GetUtilisateurUseCase(repositoryFactory);
        var utilisateur = await useCase.ExecuteAsync(id);
        
        if (utilisateur == null)
        {
            return NotFound();
        }
        
        return Ok(utilisateur);
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            Console.WriteLine($"Token reçu: {token}");

            // Afficher tous les claims pour debug
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            Console.WriteLine("Claims du token:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (email == null)
            {
                Console.WriteLine("Email non trouvé dans les claims");
                return BadRequest(new { Message = "Token invalide" });
            }

            Console.WriteLine($"Email trouvé: {email}");
        
            GetApplicationUtilisateurUseCase uc = new(repositoryFactory);
            IApplicationUtilisateur applicationUtilisateur = await uc.ExecuteAsync(email);
        
            if (applicationUtilisateur == null)
            {
                Console.WriteLine($"Utilisateur avec email {email} non trouvé");
                return NotFound(new { Message = "Utilisateur introuvable" });
            }
        
            return Ok(new { Message = "Utilisateur trouvé", Utilisateur = applicationUtilisateur });
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = "Une erreur est survenue lors de la récupération de l'utilisateur", Data = e.Message });
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UtilisateurDto utilisateurDto)
    {
        try
        {
            UpdateApplicationUtilisateurUseCase u = new(repositoryFactory);
            IApplicationUtilisateur utilisateur = await u.ExecuteAsync(utilisateurDto.ToEntity());
            return Ok(new { Message = "Utilisateur mis à jour avec succès", Utilisateur = utilisateur });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Une erreur est survenue lors de la mise à jour de l'utilisateur", Data = ex.Message });
        }
    }
    
    [HttpDelete("delete/{email}")]
    public async Task<IActionResult> Delete(string email)
    {
        try
        {
            DeleteApplicationUtilisateurUseCase uc = new(repositoryFactory, utilisateurManagementService); 
            bool result = await uc.ExecuteAsync(email);
            if (result)
                return Ok(new { Message = "Utilisateur supprimé avec succès" });
            return BadRequest(new { Message = "Une erreur est survenue lors de la suppression de l'utilisateur" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Une erreur est survenue lors de la suppression de l'utilisateur", Data = ex.Message });
        }
    }
}