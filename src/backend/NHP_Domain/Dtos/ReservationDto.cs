using NHP_Domain.Entities;

namespace NHP_Domain.Dtos;

public class ReservationDto
{
    public int NumeroReservation { get; set; }
    public DateTime ArriveeReservation { get; set; }
    public DateTime DepartReservation { get; set; }
    public int ParticipantReservation { get; set; }
    public long PrixReservation { get; set; }
    public String EtatReservation { get; set; }
    public int CartePaiement { get; set; }
    public String StatutPaiement { get; set; }

    public ReservationDto ToDto(Reservation reservation)
    {
        this.NumeroReservation = reservation.NumeroReservation;
        this.ArriveeReservation = reservation.ArriveeReservation;
        this.DepartReservation = reservation.DepartReservation;
        this.ParticipantReservation = reservation.ParticipantReservation;
        this.PrixReservation = reservation.PrixReservation;
        this.EtatReservation = reservation.EtatReservation;
        this.CartePaiement = reservation.CartePaiement;
        this.StatutPaiement = reservation.StatutPaiement;
        return this;
    }

    public List<ReservationDto> ToDtos(List<Reservation> reservations)
    {
        return reservations.Select(r => new ReservationDto().ToDto(r)).ToList();
    }

    public Reservation ToEntity()
    {
        return new Reservation
        {
            NumeroReservation = this.NumeroReservation,
            ArriveeReservation = this.ArriveeReservation,
            DepartReservation = this.DepartReservation,
            ParticipantReservation = this.ParticipantReservation,
            PrixReservation = this.PrixReservation,
            EtatReservation = this.EtatReservation,
            CartePaiement = this.CartePaiement,
            StatutPaiement = this.StatutPaiement,
        };
    }
}