using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class AdresseDto
{
    public String NomAdresse { get; set; }
    public String ComplementAdresse { get; set; }
    public String Ville  { get; set; }
    public int CodePostal { get; set; }
    public String Pays { get; set; }

    public AdresseDto ToDto(Adresse adresse)
    {
        this.NomAdresse = adresse.NomAdresse;
        this.ComplementAdresse = adresse.ComplementAdresse;
        this.Ville = adresse.Ville;
        this.CodePostal = adresse.CodePostal;
        this.Pays = adresse.Pays;
        return this;
    }

    public List<AdresseDto> ToDtos(List<Adresse> adresses)
    {
        return adresses.Select(a => new AdresseDto().ToDto(a)).ToList();
    }

    public Adresse ToEntity()
    {
        return new Adresse
        {
            NomAdresse = this.NomAdresse,
            ComplementAdresse = this.ComplementAdresse,
            Ville = this.Ville,
            CodePostal = this.CodePostal,
            Pays = this.Pays,
        };
    }
}