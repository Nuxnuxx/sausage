using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ReservationUseCases.Get;

public class GetReservationUseCase(IRepositoryFactory factory)
{
    public async Task<Reservation?> ExecuteAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(factory.ReservationRepository());
        
        return await factory.ReservationRepository().FindAsync(id);
    }
}