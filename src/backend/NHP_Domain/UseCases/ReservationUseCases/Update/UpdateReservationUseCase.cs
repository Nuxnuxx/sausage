using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ReservationUseCases.Update;

public class UpdateReservationUseCase(IRepositoryFactory factory)
{
    public async Task<Reservation?> ExecuteAsync(Reservation reservation)
    {
        await CheckBusinessRules(reservation);
        
        var existingReservation = await factory.ReservationRepository().FindAsync(reservation.Id);
        if (existingReservation == null)
            return null;
            
        Reservation updatedReservation = await factory.ReservationRepository().UpdateAsync(reservation);
        factory.ReservationRepository().SaveChangesAsync().Wait();
        return updatedReservation;
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