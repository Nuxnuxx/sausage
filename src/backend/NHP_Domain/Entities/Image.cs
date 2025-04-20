namespace NHP_Domain.Entities;

public class Image
{
    public long Id { get; set; }
    public String UrlImage { get; set; }
    public String NomImage { get; set; }
    
    // OneToMany
    //public virtual List<Logement>? Logements { get; set; }

    public override string ToString()
    {
        return $"urlImage: {UrlImage} " +
               $"nomImage: {NomImage} ";
    }
}