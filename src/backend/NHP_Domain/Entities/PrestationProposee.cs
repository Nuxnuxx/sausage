namespace NHP_Domain.Entities;

public class PrestationProposee
{
    public long IdPrestationProposee { get; set; }
    public long PrixPrestation { get; set; }
    
    //ManyToOne
    public virtual Logement? Logement { get; set; }
    public virtual Prestation? Prestation { get; set; }

    public override string ToString()
    {
        return $"nomLogement: {Logement} " +
               $"prixPrestation: {PrixPrestation} " +
               $"nomPrestation: {Prestation}" +
               $"idPrestationProposee: {IdPrestationProposee}";
    }
}