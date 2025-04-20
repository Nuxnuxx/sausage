using Microsoft.AspNetCore.Mvc;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Dtos;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.ReservationUseCases.Create;
using NHP_Domain.UseCases.ReservationUseCases.Delete;
using NHP_Domain.UseCases.ReservationUseCases.Get;
using NHP_Domain.UseCases.ReservationUseCases.Update;

namespace NHP_RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<ReservationDto>>> GetAsync()
    {
        GetAllReservationsUseCase uc = new(repositoryFactory);
        
        var reservations = (await uc.ExecuteAsync()).ToList();
        return new ReservationDto().ToDtos(reservations);
    }

    [HttpGet("{numeroReservation}")]
    public async Task<ActionResult<ReservationDto>> GetUneReservation(int numeroReservation)
    {
        GetAllReservationsUseCase uc = new(repositoryFactory);
        
        var reservations = await uc.ExecuteAsync();
        var reservation = reservations.FirstOrDefault(r => r.NumeroReservation == numeroReservation);
        
        if (reservation == null)
            return NotFound();

        return new ReservationDto().ToDto(reservation);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> PostAsync([FromBody] ReservationDto reservationDto)
    {
        CreateReservationUseCase uc = new(repositoryFactory);
        
        try
        {
            var reservation = await uc.ExecuteAsync(
                reservationDto.NumeroReservation,
                reservationDto.ArriveeReservation,
                reservationDto.DepartReservation,
                reservationDto.ParticipantReservation,
                reservationDto.PrixReservation,
                reservationDto.EtatReservation,
                reservationDto.CartePaiement,
                reservationDto.StatutPaiement
            );

            return CreatedAtAction(nameof(GetUneReservation), 
                new { numeroReservation = reservation.NumeroReservation }, 
                new ReservationDto().ToDto(reservation));
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
    }

    [HttpPut("{numeroReservation}")]
    public async Task<IActionResult> PutAsync(int numeroReservation, [FromBody] ReservationDto reservationDto)
    {
        if (numeroReservation != reservationDto.NumeroReservation)
        {
            return BadRequest();
        }
        
        UpdateReservationUseCase uc = new(repositoryFactory);
        
        try
        {
            var updatedReservation = await uc.ExecuteAsync(reservationDto.ToEntity());
            
            if (updatedReservation == null)
                return NotFound();
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        
        return NoContent();
    }

    [HttpDelete("{numeroReservation}")]
    public async Task<IActionResult> DeleteAsync(int numeroReservation)
    {
        DeleteReservationUseCase uc = new(repositoryFactory);
        try
        {
            // Find the reservation first
            GetAllReservationsUseCase getAllUc = new(repositoryFactory);
            var reservations = await getAllUc.ExecuteAsync();
            var reservation = reservations.FirstOrDefault(r => r.NumeroReservation == numeroReservation);
            
            if (reservation == null)
                return NotFound();

            var deleted = await uc.ExecuteAsync(reservation.Id);
            
            if (!deleted)
                return NotFound();
        }
        catch (Exception e)
        {
            ModelState.AddModelError(nameof(e), e.Message);
            return ValidationProblem();
        }
        return NoContent();
    }
}