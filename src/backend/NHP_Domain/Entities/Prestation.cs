namespace NHP_Domain.Entities;

public class Prestation
{
    public long Id { get; set; }
    public String NomPrestation { get; set; }
    
    // OneToMany
    public virtual List<PrestationProposee> PrestationProposees { get; set; }

    public override string ToString()
    {
        return $"nomPrestation: {NomPrestation}";
    }
}