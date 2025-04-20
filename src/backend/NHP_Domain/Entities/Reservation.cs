namespace NHP_Domain.Entities;

public class Reservation
{
    public long Id { get; set; }
    public int NumeroReservation { get; set; }
    public DateTime ArriveeReservation { get; set; }
    public DateTime DepartReservation { get; set; }
    public int ParticipantReservation { get; set; }
    public long PrixReservation { get; set; }
    public String EtatReservation { get; set; }
    public int CartePaiement { get; set; }
    public String StatutPaiement { get; set; }
    
    //ManyToOne
    public virtual Remise? Remise { get; set; }
    public virtual Logement? Logement { get; set; }
    public virtual Utilisateur? Utilisateur { get; set; }

    public override string ToString()
    {
        return $"numeroReservation : {NumeroReservation} +" +
               $"arriveeReservation : {ArriveeReservation} " +
               $"departReservation : {DepartReservation} " +
               $"participantReservation : {ParticipantReservation} " +
               $"prixReservation : {PrixReservation} " +
               $"etatReservation : {EtatReservation} " +
               $"cartePaiement : {CartePaiement} " +
               $"statutPaiement : {StatutPaiement} " +
               $"remise : {Remise} " +
               $"logement : {Logement} " +
               $"utilisateur : {Utilisateur} ";
    }
}