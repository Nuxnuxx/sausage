using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class TypeDestinationDto
{
    public String NomTypeDestination { get; set; }
    public String DescriptionTypeDestination { get; set; }
    
    public TypeDestinationDto ToDto(TypeDestination typeDestination)
    {
        this.NomTypeDestination = typeDestination.NomTypeDestination;
        this.DescriptionTypeDestination = typeDestination.DescriptionTypeDestination;
        return this;
    }

    public List<TypeDestinationDto> ToDtos(List<TypeDestination> typeDestinations)
    {
        return typeDestinations.Select(t => new TypeDestinationDto().ToDto(t)).ToList();
    }

    public TypeDestination ToEntity()
    {
        return new TypeDestination
        {
            NomTypeDestination = this.NomTypeDestination,
            DescriptionTypeDestination = this.DescriptionTypeDestination,
        };
    }
}