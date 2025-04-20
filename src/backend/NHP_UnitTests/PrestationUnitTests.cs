using System.Linq.Expressions;
using Moq;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.PrestationUseCases.Create;
using NHP_Domain.UseCases.PrestationUseCases.Delete;
using NHP_Domain.UseCases.PrestationUseCases.Get;
using NHP_Domain.UseCases.PrestationUseCases.Update;

namespace NHP_UnitTests;

public class PrestationUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreatePrestationUseCase()
    {
        long id = 1;
        string nomPrestation = "Kamehame";

        Prestation prestationAvant = new Prestation
        {
            Id = id,
            NomPrestation = nomPrestation
        };

        var mockPrestation = new Mock<IPrestationRepository>();
        mockPrestation.Setup(repo => repo.FindByConditionAsync(p => p.Id.Equals(id)))
            .ReturnsAsync((List<Prestation>)null);

        Prestation prestationFinal = new Prestation
        {
            Id = id,
            NomPrestation = nomPrestation
        };

        mockPrestation.Setup(repo => repo.CreateAsync(prestationAvant)).ReturnsAsync(prestationFinal);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationRepository()).Returns(mockPrestation.Object);

        var useCase = new CreatePrestationUseCase(mockFactory.Object);
        var prestationTest = await useCase.ExecuteAsync(prestationAvant);

        Assert.IsNotNull(prestationTest);
        Assert.That(prestationTest.Id, Is.EqualTo(id));
        Assert.That(prestationTest.NomPrestation, Is.EqualTo(nomPrestation));
    }

    [Test]
    public async Task DeletePrestationUseCase()
    {
        long id = 1;
        string nomPrestation = "kamehame";

        Prestation prestationToDelete = new Prestation
        {
            Id = id,
            NomPrestation = nomPrestation
        };

        var mockRepo = new Mock<IPrestationRepository>();
        mockRepo.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Prestation, bool>>>()))
            .ReturnsAsync(new List<Prestation>{prestationToDelete});
        mockRepo.Setup(repo => repo.DeleteAsync(prestationToDelete)).Returns(Task.CompletedTask);
        mockRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationRepository()).Returns(mockRepo.Object);

        var useCase = new DeletePrestationUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.IsTrue(result);
        mockRepo.Verify(repo => repo.DeleteAsync(prestationToDelete), Times.Once);
    }

    [Test]
    public async Task UpdatePrestationUseCase()
    {
        long id = 1;

        Prestation prestationBefore = new Prestation
        {
            Id = id,
            NomPrestation = "kamehame"
        };

        Prestation prestationToUpdate = new Prestation
        {
            Id = id,
            NomPrestation = "final flash"
        };

        var mockRepo = new Mock<IPrestationRepository>();
        mockRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(prestationBefore);
        mockRepo.Setup(repo => repo.UpdateAsync(prestationToUpdate)).ReturnsAsync(prestationToUpdate);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationRepository()).Returns(mockRepo.Object);
        mockFactory.Setup(f => f.SaveChangesAsync()).Returns(Task.CompletedTask);

        var useCase = new UpdatePrestationUseCase(mockFactory.Object);
        var updated = await useCase.ExecuteAsync(prestationToUpdate);

        Assert.IsNotNull(updated);
        Assert.That(updated.Id, Is.EqualTo(id));
        Assert.That(updated.NomPrestation, Is.EqualTo("final flash"));
    }

    [Test]
    public async Task GetPrestationUseCase()
    {
        long id = 1;

        Prestation expected = new Prestation
        {
            Id = id,
            NomPrestation = "kamehame"
        };

        var mockRepo = new Mock<IPrestationRepository>();
        mockRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(expected);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationRepository()).Returns(mockRepo.Object);

        var useCase = new GetPrestationByPrimaryKeyUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(expected.Id));
        Assert.That(result.NomPrestation, Is.EqualTo(expected.NomPrestation));
    }

    [Test]
    public async Task GetAllPrestationUseCase()
    {
        var prestations = new List<Prestation>
        {
            new Prestation
            {
                Id = 1,
                NomPrestation = "kamehame"
            },
            new Prestation
            {
                Id = 2,
                NomPrestation = "final flash"
            }
        };

        var mockRepo = new Mock<IPrestationRepository>();
        mockRepo.Setup(repo => repo.FindAllAsync()).ReturnsAsync(prestations);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationRepository()).Returns(mockRepo.Object);

        var useCase = new GetTouteLesPrestationsUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync();

        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.ElementAt(0).NomPrestation, Is.EqualTo("kamehame"));
        Assert.That(result.ElementAt(1).NomPrestation, Is.EqualTo("final flash"));

        mockRepo.Verify(repo => repo.FindAllAsync(), Times.Once);
    }
}