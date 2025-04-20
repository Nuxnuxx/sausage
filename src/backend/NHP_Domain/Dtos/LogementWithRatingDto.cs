using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class LogementWithRatingDto
{
    public long Id { get; set; }
    public string NomLogement { get; set; }
    public long PrixLogement { get; set; }
    public string DescriptionLogement { get; set; }
    public int NoteAvis { get; set; }
    public ImageDto? Image { get; set; }
    public TypeDestinationDto? Destination { get; set; }
    public AdresseDto? Adresse { get; set; }

    public static List<LogementWithRatingDto> ToDtos(List<Logement> logements, List<Avis> avis)
    {
        return logements.Select(l => new LogementWithRatingDto
        {
            Id = l.Id,
            NomLogement = l.NomLogement,
            PrixLogement = l.PrixLogement,
            DescriptionLogement = l.DescriptionLogement,
            NoteAvis = avis.Where(a => a.Logement?.Id == l.Id)
                         .Select(a => a.NoteAvis)
                         .DefaultIfEmpty(0)
                         .First(),
            Image = l.Image != null ? new ImageDto().ToDto(l.Image) : null,
            Destination = l.Destination != null ? new TypeDestinationDto().ToDto(l.Destination) : null,
            Adresse = l.Adresse != null ? new AdresseDto().ToDto(l.Adresse) : null
        }).ToList();
    }
} 