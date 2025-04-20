using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class AvisDto
{
    public long Id { get; set; }
    public int NoteAvis { get; set; }
    public String? TexteAvis { get; set; }
    
    public Logement Logement { get; set; }
    public Utilisateur Utilisateur { get; set; }

    public AvisDto ToDto(Avis avis)
    {
        this.NoteAvis = avis.NoteAvis;
        this.TexteAvis = avis.TexteAvis;
        this.Logement = avis.Logement;
        this.Utilisateur = avis.Utilisateur;
        return this;
    }

    public List<AvisDto> ToDtos(List<Avis> avis)
    {
        return avis.Select(a => new AvisDto().ToDto(a)).ToList();
    }

    public Avis ToEntity()
    {
        return new Avis
        {
            NoteAvis = this.NoteAvis,
            TexteAvis = this.TexteAvis,
            Logement = this.Logement,
            Utilisateur = this.Utilisateur
        };
    }
}