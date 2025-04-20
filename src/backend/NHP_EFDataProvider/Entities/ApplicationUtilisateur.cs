using Microsoft.AspNetCore.Identity;
using NHP_Domain.Entities;

namespace NHP_EFDataProvider.Entities;

public class ApplicationUtilisateur: IdentityUser, IApplicationUtilisateur
{
    [PersonalData]
    public virtual Utilisateur? Utilisateur { get; set; }
    [PersonalData]
    public string UtilisateurId { get; set; }
}