using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class UtilisateurDto
{
    public String? Id { get; set; }
    public String MailUtilisateur { get; set; }
    public String NomUtilisateur { get; set; }
    public String PrenomUtilisateur { get; set; }
    public String? MdpUtilisateur { get; set; }
    public String CiviliteUtilisateur { get; set; }
    public String TelephoneUtilisateur { get; set; }
    public bool NewsLetterUtilisateur { get; set; }

    public UtilisateurDto ToDto(Utilisateur utilisateur)
    {
        this.MailUtilisateur = utilisateur.MailUtilisateur;
        this.NomUtilisateur = utilisateur.NomUtilisateur;
        this.PrenomUtilisateur = utilisateur.PrenomUtilisateur;
        this.MdpUtilisateur = this.MdpUtilisateur;
        this.CiviliteUtilisateur = this.CiviliteUtilisateur;
        this.TelephoneUtilisateur = this.TelephoneUtilisateur;
        this.NewsLetterUtilisateur = this.NewsLetterUtilisateur;
        return this;
    }

    public List<UtilisateurDto> ToDtos(List<Utilisateur> utilisateurs)
    {
        return utilisateurs.Select(u => new UtilisateurDto().ToDto(u)).ToList();
    }

    public Utilisateur ToEntity()
    {
        return new Utilisateur
        {
            Id = this.Id,
            MailUtilisateur = this.MailUtilisateur,
            NomUtilisateur = this.NomUtilisateur,
            PrenomUtilisateur = this.PrenomUtilisateur,
            CiviliteUtilisateur = this.CiviliteUtilisateur,
            TelephoneUtilisateur = this.TelephoneUtilisateur,
            NewsLetterUtilisateur = this.NewsLetterUtilisateur
        };
    }
}