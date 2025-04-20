namespace NHP_Domain.Dtos;

public class InscriptionDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string? Civilite { get; set; }
    public string? Telephone { get; set; }
    public bool Newsletter { get; set; }
}