namespace NHP_Domain.Entities;

public class TypeDestination
{
    public long Id { get; set; }
    public String NomTypeDestination { get; set; } = String.Empty;
    public String? DescriptionTypeDestination { get; set; }
    
    // OneToMany
    //public virtual List<Logement>? Logements { get; set; } = new();

    public override string ToString()
    {
        return $"typeDestination: {NomTypeDestination} " +
               $"descriptionTypeDestination: {DescriptionTypeDestination}";
    }
}