using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class RemiseDto
{
    public String NomRemise { get; set; }
    public String CodeRemise { get; set; }
    public int PourcentageRemise { get; set; }

    public RemiseDto ToDto(Remise remise)
    {
        this.NomRemise = remise.NomRemise;
        this.CodeRemise = remise.CodeRemise;
        this.PourcentageRemise = remise.PourcentageRemise;
        return this;
    }

    public List<RemiseDto> ToDtos(List<Remise> remise)
    {
        return remise.Select(r => new RemiseDto().ToDto(r)).ToList();
    }

    public Remise ToEntity()
    {
        return new Remise
        {
            NomRemise = this.NomRemise,
            CodeRemise = this.CodeRemise,
            PourcentageRemise = this.PourcentageRemise,
        };
    }
}