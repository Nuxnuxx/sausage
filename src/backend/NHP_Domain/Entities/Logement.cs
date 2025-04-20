namespace NHP_Domain.Entities;

public class Logement
{
    public long Id { get; set; }
    public String NomLogement { get; set; }
    public long PrixLogement { get; set; }
    public String DescriptionLogement { get; set; }
    
    // ManyToOne
    public virtual Image? Image { get; set; }
    public virtual TypeDestination? Destination { get; set; }
    public virtual Adresse? Adresse { get; set; }

    public override string ToString()
    {
        return $"nomLogement: {NomLogement} " +
               $"prix: {PrixLogement} " +
               $"descriptionLogement: {DescriptionLogement} " +
               $"urlImage: {Image} " +
               $"typeDestination: {Destination} " +
               $"adresse: {Adresse}";
    }
}