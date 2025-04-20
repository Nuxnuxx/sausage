using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class PrestationDto
{
    public long Id { get; set; }
    public String NomPrestation { get; set; }

    public PrestationDto ToDto(Prestation prestation)
    {
        this.NomPrestation = prestation.NomPrestation;
        return this;
    }

    public List<PrestationDto> ToDtos(List<Prestation> prestations)
    {
        return prestations.Select(p => new PrestationDto().ToDto(p)).ToList();
    }

    public Prestation ToEntity()
    {
        return new Prestation
        {
            NomPrestation = this.NomPrestation,
        };
    }
}