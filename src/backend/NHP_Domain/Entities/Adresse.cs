namespace NHP_Domain.Entities;

public class Adresse
{
    public long Id { get; set; }
    public required String NomAdresse { get; set; }
    public String? ComplementAdresse { get; set; }
    public required String Ville  { get; set; }
    public required int CodePostal { get; set; }
    public required String Pays { get; set; }
    
    // OneToMany
    //public virtual List<Logement>? Logements { get; set; }
    //public virtual List<Utilisateur>? Utilisateurs { get; set; }

    public override string ToString()
    {
        return $"NomAdresse: {NomAdresse} " +
               $"complementAdresse: {ComplementAdresse} " +
               $"ville: {Ville} " +
               $"codePostal: {CodePostal} " +
               $"pays: {Pays} ";
    }
}