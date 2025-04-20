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

public class PrestationProposeeUnitTests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public async Task CreatePrestationProposeeUseCase()
    {
        long id = 1;
        long prix = 250;

        var logement = new Logement { Id = 1, NomLogement = "palais de jade" };
        var prestation = new Prestation { Id = 1, NomPrestation = "kamehame" };

        PrestationProposee avant = new PrestationProposee
        {
            IdPrestationProposee = id,
            PrixPrestation = prix,
            Logement = logement,
            Prestation = prestation
        };

        var mockRepo = new Mock<IPrestationProposeeRepository>();
        mockRepo.Setup(r => r.FindByConditionAsync(p => p.IdPrestationProposee.Equals(id)))
            .ReturnsAsync((List<PrestationProposee>)null);
        mockRepo.Setup(r => r.CreateAsync(avant)).ReturnsAsync(avant);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationProposeeRepository()).Returns(mockRepo.Object);

        var useCase = new CreatePrestationProposeeUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(avant);

        Assert.IsNotNull(result);
        Assert.That(result.IdPrestationProposee, Is.EqualTo(id));
        Assert.That(result.PrixPrestation, Is.EqualTo(prix));
        Assert.That(result.Logement, Is.EqualTo(logement));
        Assert.That(result.Prestation, Is.EqualTo(prestation));
    }
    
    [Test]
    public async Task DeletePrestationProposeeUseCase()
    {
        long id = 1;

        var toDelete = new PrestationProposee
        {
            IdPrestationProposee = id,
            PrixPrestation = 250,
            Logement = new Logement { Id = 1 },
            Prestation = new Prestation { Id = 1 }
        };

        var mockRepo = new Mock<IPrestationProposeeRepository>();
        mockRepo.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<PrestationProposee, bool>>>()))
            .ReturnsAsync(new List<PrestationProposee>{toDelete});
        mockRepo.Setup(r => r.DeleteAsync(toDelete)).Returns(Task.CompletedTask);
        mockRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationProposeeRepository()).Returns(mockRepo.Object);

        var useCase = new DeletePrestationProposeeUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.IsTrue(result);
        mockRepo.Verify(r => r.DeleteAsync(toDelete), Times.Once);
    }

    [Test]
    public async Task UpdatePrestationProposeeUseCase()
    {
        long id = 1;

        var before = new PrestationProposee
        {
            IdPrestationProposee = id,
            PrixPrestation = 250,
            Logement = new Logement { Id = 1 },
            Prestation = new Prestation { Id = 1 }
        };

        var updated = new PrestationProposee
        {
            IdPrestationProposee = id,
            PrixPrestation = 400, // Mise à jour du prix
            Logement = new Logement { Id = 1 },
            Prestation = new Prestation { Id = 1 }
        };

        var mockRepo = new Mock<IPrestationProposeeRepository>();
        mockRepo.Setup(r => r.FindAsync(id)).ReturnsAsync(before);
        mockRepo.Setup(r => r.UpdateAsync(updated)).ReturnsAsync(updated);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationProposeeRepository()).Returns(mockRepo.Object);
        mockFactory.Setup(f => f.SaveChangesAsync()).Returns(Task.CompletedTask);

        var useCase = new UpdatePrestationProposeeUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(updated);

        Assert.IsNotNull(result);
        Assert.That(result.IdPrestationProposee, Is.EqualTo(id));
        Assert.That(result.PrixPrestation, Is.EqualTo(400));
    }
    
    [Test]
    public async Task GetPrestationProposeeUseCase()
    {
        long id = 1;

        var expected = new PrestationProposee
        {
            IdPrestationProposee = id,
            PrixPrestation = 300,
            Logement = new Logement { Id = 1 },
            Prestation = new Prestation { Id = 1 }
        };

        var mockRepo = new Mock<IPrestationProposeeRepository>();
        mockRepo.Setup(r => r.FindAsync(id)).ReturnsAsync(expected);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationProposeeRepository()).Returns(mockRepo.Object);

        var useCase = new GetPrestationProposeeByPrimaryKeyUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.IsNotNull(result);
        Assert.That(result.IdPrestationProposee, Is.EqualTo(id));
        Assert.That(result.PrixPrestation, Is.EqualTo(300));
    }
    
    [Test]
    public async Task GetAllPrestationProposeeUseCase()
    {
        var list = new List<PrestationProposee>
        {
            new PrestationProposee
            {
                IdPrestationProposee = 1,
                PrixPrestation = 300,
                Logement = new Logement { Id = 1 },
                Prestation = new Prestation { Id = 1 }
            },
            new PrestationProposee
            {
                IdPrestationProposee = 2,
                PrixPrestation = 500,
                Logement = new Logement { Id = 2 },
                Prestation = new Prestation { Id = 2 }
            }
        };

        var mockRepo = new Mock<IPrestationProposeeRepository>();
        mockRepo.Setup(r => r.FindAllAsync()).ReturnsAsync(list);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.PrestationProposeeRepository()).Returns(mockRepo.Object);

        var useCase = new GetTouteLesPrestationsProposeeUseCase(mockFactory.Object);
        var result = await useCase.ExecuteAsync();

        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.ElementAt(0).PrixPrestation, Is.EqualTo(300));
        Assert.That(result.ElementAt(1).PrixPrestation, Is.EqualTo(500));

        mockRepo.Verify(r => r.FindAllAsync(), Times.Once);
    }

}