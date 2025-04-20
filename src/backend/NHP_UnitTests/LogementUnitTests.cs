using System.Diagnostics.Eventing.Reader;
using Moq;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.LogementUseCases.Create;
using NHP_Domain.UseCases.LogementUseCases.Delete;
using NHP_Domain.UseCases.LogementUseCases.Get;
using NHP_Domain.UseCases.LogementUseCases.Update;

namespace NHP_UnitTests;

public class LogementUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateLogementUseCase()
    {
        long id = 1;
        string nomLogement = "palais de jade";
        long prixLogement = 1000;
        string descriptionLogement = "c'est encore une ref";

        Logement logementAvant = new Logement
        {
            Id = id,
            NomLogement = nomLogement,
            PrixLogement = prixLogement,
            DescriptionLogement = descriptionLogement,
        };

        var mockLogement = new Mock<ILogementRepository>();
        mockLogement.Setup(repo => repo.FindByConditionAsync(l => l.Id.Equals(id)))
            .ReturnsAsync((List<Logement>)null);

        Logement logementFinal = new Logement
        {
            Id = id,
            NomLogement = nomLogement,
            PrixLogement = prixLogement,
            DescriptionLogement = descriptionLogement
        };

        mockLogement.Setup(repo => repo.CreateAsync(logementAvant)).ReturnsAsync(logementFinal);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(facto => facto.LogementRepository()).Returns(mockLogement.Object);
        CreateLogementUseCase uc = new CreateLogementUseCase(mockFactory.Object);

        var logementTest = await uc.ExecuteAsync(logementAvant);

        Assert.IsNotNull(logementTest);
        Assert.That(logementTest.Id, Is.EqualTo(logementAvant.Id));
        Assert.That(logementTest.NomLogement, Is.EqualTo(logementAvant.NomLogement));
        Assert.That(logementTest.PrixLogement, Is.EqualTo(logementAvant.PrixLogement));
        Assert.That(logementTest.DescriptionLogement, Is.EqualTo(descriptionLogement));

    }

    [Test]
    public async Task DeleteLogementUseCase()
    {
        long id = 1;
        string nomLogement = "palais de jade";
        long prixLogement = 1000;
        string descriptionLogement = "c'est encore une ref";

        Logement logementToDelete = new Logement
        {
            Id = id,
            NomLogement = nomLogement,
            PrixLogement = prixLogement,
            DescriptionLogement = descriptionLogement
        };

        var mockLogementRepo = new Mock<ILogementRepository>();

        mockLogementRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(logementToDelete);
        mockLogementRepo.Setup(repo => repo.DeleteAsync(logementToDelete)).Returns(Task.CompletedTask);
        mockLogementRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(l => l.LogementRepository()).Returns(mockLogementRepo.Object);

        var useCase = new DeleteLogementUseCase(mockFactory.Object);

        bool result = await useCase.ExecuteAsync(id);

        Assert.IsTrue(result);
        mockLogementRepo.Verify(repo => repo.DeleteAsync(logementToDelete), Times.Once);
    }

    [Test]
    public async Task UpdateLogementUseCase()
    {
        long id = 1;

        Logement logementBeforeUpdate = new Logement
        {
            Id = id,
            NomLogement = "palais de jade",
            PrixLogement = 1000,
            DescriptionLogement = "c'est encore une ref",
        };

        Logement logementToUpdate = new Logement
        {
            Id = id,
            NomLogement = "palais de jade",
            PrixLogement = 10000, // En fait j'avais oublié un 0 (citation du client)
            DescriptionLogement = "c'est encore une ref",
        };

        var mockLogementRepo = new Mock<ILogementRepository>();
        mockLogementRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(logementBeforeUpdate);
        mockLogementRepo.Setup(repo => repo.UpdateAsync(logementToUpdate)).ReturnsAsync(logementToUpdate);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(l => l.LogementRepository()).Returns(mockLogementRepo.Object);
        mockFactory.Setup(l => l.SaveChangesAsync()).Returns(Task.CompletedTask);

        var useCase = new UpdateLogementUseCase(mockFactory.Object);

        var logementUpdated = await useCase.ExecuteAsync(logementToUpdate);
        Assert.IsNotNull(logementUpdated);
        Assert.That(logementUpdated.Id, Is.EqualTo(id));
        Assert.That(logementUpdated.NomLogement, Is.EqualTo("palais de jade"));
        Assert.That(logementUpdated.PrixLogement, Is.EqualTo(10000));
        Assert.That(logementUpdated.DescriptionLogement, Is.EqualTo("c'est encore une ref"));
    }

    [Test]
    public async Task GetLogementUseCase()
    {
        long id = 1;

        Logement expectedLogement = new Logement
        {
            Id = id,
            NomLogement = "palais de jade",
            PrixLogement = 1000,
            DescriptionLogement = "c'est encore une ref",
        };

        var mockLogementRepo = new Mock<ILogementRepository>();
        mockLogementRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(expectedLogement);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(l => l.LogementRepository()).Returns(mockLogementRepo.Object);

        var useCase = new GetLogementUseCase(mockFactory.Object);

        var result = await useCase.ExecuteAsync(id);

        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(expectedLogement.Id));
        Assert.That(result.NomLogement, Is.EqualTo(expectedLogement.NomLogement));
        Assert.That(result.PrixLogement, Is.EqualTo(expectedLogement.PrixLogement));
        Assert.That(result.DescriptionLogement, Is.EqualTo(expectedLogement.DescriptionLogement));
    }
    
    [Test]
    public async Task GetAllLogementUseCase()
    {
        var mockLogements = new List<Logement>
        {
            new Logement
            {
                Id = 1,
                NomLogement = "palais de jade",
                PrixLogement = 1000,
                DescriptionLogement = "c'est encore une ref",
            },

            new Logement
            {
                Id = 2,
                NomLogement = "palais vert de gris",
                PrixLogement = 2000,
                DescriptionLogement = "c'est une deuxième ref",
            }
        };

        var mockLogementRepo = new Mock<ILogementRepository>();
        mockLogementRepo.Setup(repo => repo.FindAllAsync()).ReturnsAsync(mockLogements);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(l => l.LogementRepository()).Returns(mockLogementRepo.Object);

        var useCase = new GetAllLogementsUseCase(mockFactory.Object);

        var results = await useCase.ExecuteAsync();

        Assert.IsNotNull(results);
        Assert.That(results.Count, Is.EqualTo(2));
        Assert.That(results.ElementAt(0).NomLogement, Is.EqualTo("palais de jade"));
        Assert.That(results.ElementAt(1).NomLogement, Is.EqualTo("palais vert de gris"));

        mockLogementRepo.Verify(repo => repo.FindAllAsync(), Times.Once);
    }
}