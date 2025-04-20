using System.Runtime.InteropServices.JavaScript;

namespace NHP_Domain.Entities;

public class Utilisateur
{
    public required String Id { get; set; }
    public required String MailUtilisateur { get; set; }
    public String? NomUtilisateur { get; set; }
    public String? PrenomUtilisateur { get; set; }
    public String? CiviliteUtilisateur { get; set; }
    public String? TelephoneUtilisateur { get; set; }
    public bool NewsLetterUtilisateur { get; set; }
    
    //ManyToOne
    //public virtual Role? Role { get; set; }
    public virtual Adresse? Adresse { get; set; }
    
    //OneToMany
    //public virtual List<Reservation> Reservation { get; set; }
    //public virtual List<Avis> Avis { get; set; }

    public override string ToString()
    {
        return $"mailUtilisateur: {MailUtilisateur} " +
               $"nomUtilisateur: {NomUtilisateur} " +
               $"prenomUtilisateur: {PrenomUtilisateur} " +
               $"civiliteUtilisateur: {CiviliteUtilisateur}" +
               $"telephoneUtilisateur: {TelephoneUtilisateur}" +
               $"newsLetterUtilisateur: {NewsLetterUtilisateur}" +
               //$"role: {Role}" +
               $"adresse: {Adresse}";
    }
}