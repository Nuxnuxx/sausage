namespace NHP_Domain.Entities;

public class Remise
{
    public long Id { get; set; }
    public String NomRemise { get; set; } = String.Empty;
    public String CodeRemise { get; set; } = String.Empty;
    public int PourcentageRemise { get; set; } = 1;
    
    //OneToMany
    public virtual List<Reservation>? Reservations { get; set; } = new();

    public override string ToString()
    {
        return $"nomRemise: {NomRemise} +" +
               $" pourcentage: {PourcentageRemise} +" +
               $" codeRemise: {CodeRemise}";
    }
}