using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ReservationUseCases.Create;

public class CreateReservationUseCase(IRepositoryFactory factory)
{
    public async Task<Reservation> ExecuteAsync(int numeroReservation, DateTime arriveeReservation, 
        DateTime departReservation, int participantReservation, long prixReservation, 
        String etatReservation, int cartePaiement, String statutPaiement, 
        Remise? remise = null, Logement? logement = null, Utilisateur? utilisateur = null)
    {
        var reservation = new Reservation
        {
            NumeroReservation = numeroReservation,
            ArriveeReservation = arriveeReservation,
            DepartReservation = departReservation,
            ParticipantReservation = participantReservation,
            PrixReservation = prixReservation,
            EtatReservation = etatReservation,
            CartePaiement = cartePaiement,
            StatutPaiement = statutPaiement,
            Remise = remise,
            Logement = logement,
            Utilisateur = utilisateur
        };
        
        return await ExecuteAsync(reservation);
    }

    public async Task<Reservation> ExecuteAsync(Reservation reservation)
    {
        await CheckBusinessRules(reservation);
        Reservation result = await factory.ReservationRepository().CreateAsync(reservation);
        factory.ReservationRepository().SaveChangesAsync().Wait();
        return result;
    }

    private async Task CheckBusinessRules(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        ArgumentNullException.ThrowIfNull(reservation.EtatReservation);
        ArgumentNullException.ThrowIfNull(reservation.StatutPaiement);
        
        if (reservation.NumeroReservation <= 0)
            throw new ArgumentException("Le numéro de réservation doit être supérieur à 0");
            
        if (reservation.ArriveeReservation >= reservation.DepartReservation)
            throw new ArgumentException("La date d'arrivée doit être antérieure à la date de départ");
            
        if (reservation.ParticipantReservation <= 0)
            throw new ArgumentException("Le nombre de participants doit être supérieur à 0");
            
        if (reservation.PrixReservation <= 0)
            throw new ArgumentException("Le prix de la réservation doit être supérieur à 0");
            
        if (reservation.CartePaiement <= 0)
            throw new ArgumentException("Le numéro de carte de paiement doit être valide");
            
        ArgumentNullException.ThrowIfNull(factory.ReservationRepository());
    }
}
