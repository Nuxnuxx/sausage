using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ReservationUseCases.Delete;

public class DeleteReservationUseCase(IRepositoryFactory factory)
{
    public async Task<bool> ExecuteAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(factory.ReservationRepository());
        
        var reservation = await factory.ReservationRepository().FindAsync(id);
        if (reservation == null)
            return false;
            
        await factory.ReservationRepository().DeleteAsync(reservation);
        factory.ReservationRepository().SaveChangesAsync().Wait();
        return true;
    }
}