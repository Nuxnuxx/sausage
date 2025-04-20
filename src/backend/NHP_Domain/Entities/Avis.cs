namespace NHP_Domain.Entities;

public class Avis
{
    public long Id { get; set; }
    public int NoteAvis { get; set; }
    public String? TexteAvis { get; set; } // falcultatif
    
    //ManyToOne
    public virtual required Logement Logement { get; set; }
    public virtual required Utilisateur Utilisateur { get; set; }

    public override string ToString()
    {
        return $"noteAvis: {NoteAvis} " +
               $"texteAvis: {TexteAvis} " +
               $"logement: {Logement}" +
               $"utilisateur: {Utilisateur}";
    }
}