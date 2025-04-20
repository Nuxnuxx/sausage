using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class PrestationProposeeDto
{
    public long IdPrestationProposee { get; set; }
    public long PrixPrestation { get; set; }

    public PrestationProposeeDto ToDto(PrestationProposee prestationProposee)
    {
        this.PrixPrestation = prestationProposee.PrixPrestation;
        return this;
    }

    public List<PrestationProposeeDto> ToDtos(List<PrestationProposee> prestationProposées)
    {
        return prestationProposées.Select(p => new PrestationProposeeDto().ToDto(p)).ToList();
    }

    public PrestationProposee ToEntity()
    {
        return new PrestationProposee
        {
            PrixPrestation = this.PrixPrestation,
        };
    }
}