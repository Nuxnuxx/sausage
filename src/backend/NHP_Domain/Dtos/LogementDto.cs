using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class LogementDto
{
    public long Id { get; set; }
    public string NomLogement { get; set; }
    public long PrixLogement { get; set; }
    public string DescriptionLogement { get; set; }
    public ImageDto? Image { get; set; }
    public TypeDestinationDto? Destination { get; set; }
    public AdresseDto? Adresse { get; set; }

    public Logement ToEntity()
    {
        return new Logement
        {
            Id = Id,
            NomLogement = NomLogement,
            PrixLogement = PrixLogement,
            DescriptionLogement = DescriptionLogement,
            Image = Image?.ToEntity(),
            Destination = Destination?.ToEntity(),
            Adresse = Adresse?.ToEntity()
        };
    }

    public LogementDto ToDto(Logement logement)
    {
        return new LogementDto
        {
            Id = logement.Id,
            NomLogement = logement.NomLogement,
            PrixLogement = logement.PrixLogement,
            DescriptionLogement = logement.DescriptionLogement,
            Image = logement.Image != null ? new ImageDto().ToDto(logement.Image) : null,
            Destination = logement.Destination != null ? new TypeDestinationDto().ToDto(logement.Destination) : null,
            Adresse = logement.Adresse != null ? new AdresseDto().ToDto(logement.Adresse) : null
        };
    }

    public List<LogementDto> ToDtos(List<Logement> logements)
    {
        return logements.Select(ToDto).ToList();
    }
}