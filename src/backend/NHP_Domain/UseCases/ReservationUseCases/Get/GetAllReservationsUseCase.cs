using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;

namespace NHP_Domain.UseCases.ReservationUseCases.Get;

public class GetAllReservationsUseCase(IRepositoryFactory factory)
{
    public async Task<IEnumerable<Reservation>> ExecuteAsync()
    {
        ArgumentNullException.ThrowIfNull(factory.ReservationRepository());
        
        return await factory.ReservationRepository().FindAllAsync();
    }
}