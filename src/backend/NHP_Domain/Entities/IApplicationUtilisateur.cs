namespace NHP_Domain.Entities;

public interface IApplicationUtilisateur
{
    string? UtilisateurId { get; set; }
    Utilisateur? Utilisateur { get; set; }
}