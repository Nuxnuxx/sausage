using Moq;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.ReservationUseCases.Create;
using NHP_Domain.UseCases.ReservationUseCases.Delete;
using NHP_Domain.UseCases.ReservationUseCases.Get;
using NHP_Domain.UseCases.ReservationUseCases.Update;

namespace NHP_UnitTests;

public class ReservationUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateReservationUseCase()
    {
        long id = 1;
        var now = DateTime.Now;

        Reservation reservation = new Reservation
        {
            Id = id,
            NumeroReservation = 123456,
            ArriveeReservation = now,
            DepartReservation = now.AddDays(3),
            ParticipantReservation = 2,
            PrixReservation = 500,
            EtatReservation = "Confirmée",
            CartePaiement = 1234,
            StatutPaiement = "Payée",
        };

        var mockRepo = new Mock<IReservationRepository>();
        mockRepo.Setup(r => r.FindByConditionAsync(r => r.Id.Equals(id))).ReturnsAsync((List<Reservation>)null);
        mockRepo.Setup(r => r.CreateAsync(reservation)).ReturnsAsync(reservation);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.ReservationRepository()).Returns(mockRepo.Object);

        var useCase = new CreateReservationUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(reservation);

        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(id));
        Assert.That(result.NumeroReservation, Is.EqualTo(123456));
        Assert.That(result.EtatReservation, Is.EqualTo("Confirmée"));
        Assert.That(result.StatutPaiement, Is.EqualTo("Payée"));
    }

    [Test]
    public async Task DeleteReservationUseCase()
    {
        long id = 1;

        var toDelete = new Reservation
        {
            Id = id,
            NumeroReservation = 123456,
        };

        var mockRepo = new Mock<IReservationRepository>();
        mockRepo.Setup(r => r.FindAsync(id)).ReturnsAsync(toDelete);
        mockRepo.Setup(r => r.DeleteAsync(toDelete)).Returns(Task.CompletedTask);
        mockRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.ReservationRepository()).Returns(mockRepo.Object);

        var useCase = new DeleteReservationUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.IsTrue(result);
        mockRepo.Verify(r => r.DeleteAsync(toDelete), Times.Once);
    }
    
    [Test]
    public async Task UpdateReservationUseCase()
    {
        long id = 1;

        var reservationBefore = new Reservation
        {
            Id = id,
            NumeroReservation = 123456,
            PrixReservation = 500,
            EtatReservation = "En attente",
            StatutPaiement = "En cours",
            ArriveeReservation = new DateTime(2025, 04, 20),
            DepartReservation = new DateTime(2025, 04, 25),
            ParticipantReservation = 2,
            CartePaiement = 1234567
        };

        var reservationAfter = new Reservation
        {
            Id = id,
            NumeroReservation = 123456,
            PrixReservation = 700,
            EtatReservation = "Confirmée",
            StatutPaiement = "Payée",
            ArriveeReservation = new DateTime(2025, 04, 20),
            DepartReservation = new DateTime(2025, 04, 25),
            ParticipantReservation = 2,
            CartePaiement = 1234567
        };

        var mockRepo = new Mock<IReservationRepository>();
        mockRepo.Setup(r => r.FindAsync(id)).ReturnsAsync(reservationBefore);
        mockRepo.Setup(r => r.UpdateAsync(reservationAfter)).ReturnsAsync(reservationAfter);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.ReservationRepository()).Returns(mockRepo.Object);
        mockFactory.Setup(f => f.SaveChangesAsync()).Returns(Task.CompletedTask);

        var useCase = new UpdateReservationUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(reservationAfter);

        Assert.IsNotNull(result);
        Assert.That(result.PrixReservation, Is.EqualTo(700));
        Assert.That(result.EtatReservation, Is.EqualTo("Confirmée"));
        Assert.That(result.StatutPaiement, Is.EqualTo("Payée"));
    }

    [Test]
    public async Task GetReservationUseCase()
    {
        long id = 1;

        var expected = new Reservation
        {
            Id = id,
            NumeroReservation = 123456,
            EtatReservation = "Confirmée"
        };

        var mockRepo = new Mock<IReservationRepository>();
        mockRepo.Setup(r => r.FindAsync(id)).ReturnsAsync(expected);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.ReservationRepository()).Returns(mockRepo.Object);

        var useCase = new GetReservationUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(id));
        Assert.That(result.NumeroReservation, Is.EqualTo(123456));
        Assert.That(result.EtatReservation, Is.EqualTo("Confirmée"));
    }
    
    [Test]
    public async Task GetAllReservationUseCase()
    {
        var list = new List<Reservation>
        {
            new Reservation
            {
                Id = 1,
                NumeroReservation = 123456,
                EtatReservation = "Confirmée"
            },
            new Reservation
            {
                Id = 2,
                NumeroReservation = 654321,
                EtatReservation = "Annulée"
            }
        };

        var mockRepo = new Mock<IReservationRepository>();
        mockRepo.Setup(r => r.FindAllAsync()).ReturnsAsync(list);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.ReservationRepository()).Returns(mockRepo.Object);

        var useCase = new GetAllReservationsUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync();

        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.ElementAt(0).NumeroReservation, Is.EqualTo(123456));
        Assert.That(result.ElementAt(1).NumeroReservation, Is.EqualTo(654321));

        mockRepo.Verify(r => r.FindAllAsync(), Times.Once);
    }

}