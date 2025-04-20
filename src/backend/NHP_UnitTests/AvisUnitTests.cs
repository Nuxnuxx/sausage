using System.Linq.Expressions;
using Moq;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.AvisExceptions;
using NHP_Domain.UseCases.AvisUseCases.Create;
using NHP_Domain.UseCases.AvisUseCases.Delete;
using NHP_Domain.UseCases.AvisUseCases.Get;
using NHP_Domain.UseCases.AvisUseCases.Update;

namespace NHP_UnitTests;

public class AvisUnitTest
{
    private Mock<IRepositoryFactory> _repositoryFactoryMock;
    private Mock<IAvisRepository> _avisRepositoryMock;
    private Mock<ILogementRepository> _logementRepositoryMock;
    private Mock<IUtilisateurRepository> _utilisateurRepositoryMock;
    private Mock<IReservationRepository> _reservationRepositoryMock;

    // données simulées
    private static readonly Logement MockLogement = new Logement { NomLogement = "Test Logement" };
    private static readonly Utilisateur MockUtilisateur = new Utilisateur
    {
        MailUtilisateur = "test@exemple.com",
        Id = "15"
    };
    private static readonly Avis MockAvis = new Avis { NoteAvis = 5, TexteAvis = "Très bon séjour", Logement = MockLogement, Utilisateur = MockUtilisateur };
    
    [SetUp]
    public void Setup()
    {
        // Initialisation des mocks pour chaque test
        _avisRepositoryMock = new Mock<IAvisRepository>();
        _logementRepositoryMock = new Mock<ILogementRepository>();
        _utilisateurRepositoryMock = new Mock<IUtilisateurRepository>();
        _reservationRepositoryMock = new Mock<IReservationRepository>();
        _repositoryFactoryMock = new Mock<IRepositoryFactory>();
        
        _logementRepositoryMock.Setup(r => r.FindAsync(It.IsAny<string>())).ReturnsAsync(MockLogement);
        _utilisateurRepositoryMock.Setup(r => r.FindAsync(It.IsAny<string>())).ReturnsAsync(MockUtilisateur);

        _repositoryFactoryMock.Setup(rf => rf.AvisRepository()).Returns(_avisRepositoryMock.Object);
        _repositoryFactoryMock.Setup(rf => rf.LogementRepository()).Returns(_logementRepositoryMock.Object);
        _repositoryFactoryMock.Setup(rf => rf.UtilisateurRepository()).Returns(_utilisateurRepositoryMock.Object);
        _repositoryFactoryMock.Setup(rf => rf.ReservationRepository()).Returns(_reservationRepositoryMock.Object);
    }

    // Test pour vérifier la création d'un avis valide
    [Test]
    public async Task CreateAvisUseCase_Valid()
    {
        var useCase = new CreateAvisUseCase(_repositoryFactoryMock.Object);
        
        _avisRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Avis>()))
            .ReturnsAsync(MockAvis);

        var result = await useCase.ExecuteAsync(MockAvis);

        Assert.IsNotNull(result);
        Assert.That(result.NoteAvis, Is.EqualTo(5));
    }

    // Test pour vérifier que la création d'un avis si un avis existe déjà pour le même utilisateur et logement
    [Test]
    public void CreateAvisUseCase_AvisExists()
    {
        var useCase = new CreateAvisUseCase(_repositoryFactoryMock.Object);

        _logementRepositoryMock
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Logement, bool>>>()))
            .ReturnsAsync(new List<Logement> { MockLogement });

        _avisRepositoryMock
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Avis, bool>>>()))
            .ReturnsAsync(new List<Avis> { MockAvis });

        Assert.ThrowsAsync<AlreadyExistException>(async () =>
            await useCase.ExecuteAsync(5, "Très bon séjour", "Test Logement", "test@exemple.com"));
    }

    // Test pour vérifier que la création d'un avis échoue si la note est invalide
    [Test]
    public void CreateAvisUseCase_NoteInvalid()
    {
        var useCase = new CreateAvisUseCase(_repositoryFactoryMock.Object);

        _logementRepositoryMock
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Logement, bool>>>()))
            .ReturnsAsync(new List<Logement> { MockLogement });

        _avisRepositoryMock
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Avis, bool>>>()))
            .ReturnsAsync(new List<Avis>());

        Assert.ThrowsAsync<InvalidAvisException>(async () =>
            await useCase.ExecuteAsync(0, "Très bon séjour", "Test Logement", "test@exemple.com"));
    }

    // Test pour vérifier que la création d'un avis échoue si le texte de l'avis est trop court
    [Test]
    public void CreateAvisUseCase_TextTooShort()
    {
        var useCase = new CreateAvisUseCase(_repositoryFactoryMock.Object);

        _logementRepositoryMock
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Logement, bool>>>()))
            .ReturnsAsync(new List<Logement> { MockLogement });

        _avisRepositoryMock
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Avis, bool>>>()))
            .ReturnsAsync(new List<Avis>());

        Assert.ThrowsAsync<TooShortException>(async () =>
            await useCase.ExecuteAsync(5, "Bon", "Test Logement", "test@exemple.com"));
    }

    // Test pour vérifier la récupération d'un avis
    [Test]
    public async Task GetAvisUseCase()
    {
        var useCase = new GetAvisUseCase(_repositoryFactoryMock.Object);
        var avis = new Avis { NoteAvis = 5, TexteAvis = "Très bon séjour", Logement = new Logement { NomLogement = "Test Logement" }, Utilisateur = new Utilisateur
            {
                MailUtilisateur = "test@exemple.com",
                Id = "18"
            }
        };

        _avisRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Avis, bool>>>())).ReturnsAsync(new List<Avis> { avis });

        var result = await useCase.ExecuteAsync("Test Logement", "test@exemple.com");

        Assert.IsNotNull(result);
        Assert.That(result.NoteAvis, Is.EqualTo(5));
        _avisRepositoryMock.Verify(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Avis, bool>>>()), Times.Once);
    }

    // Test pour vérifier la mise à jour d'un avis
    [Test]
    public async Task UpdateAvisUseCase_Valid()
    {
        var useCase = new UpdateAvisUseCase(_repositoryFactoryMock.Object);
        var avis = new Avis
        {
            NoteAvis = 5,
            TexteAvis = "Très bon séjour",
            Logement = new Logement { NomLogement = "Test Logement" },
            Utilisateur = new Utilisateur
            {
                MailUtilisateur = "test@exemple.com",
                Id = "17"
            }
        };

        _avisRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Avis>()))
            .ReturnsAsync(avis);

        var result = await useCase.ExecuteAsync(avis);

        Assert.IsNotNull(result);
        Assert.That(result.NoteAvis, Is.EqualTo(5));
        _avisRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Avis>()), Times.Once);
    }

    // Test pour vérifier la suppression d'un avis
    [Test]
    public async Task DeleteAvisUseCase_AvisExists()
    {
        var useCase = new DeleteAvisUseCase(_repositoryFactoryMock.Object);
        var avis = new Avis
        {
            NoteAvis = 5,
            TexteAvis = "Très bon séjour",
            Logement = new Logement { NomLogement = "Test Logement" },
            Utilisateur = new Utilisateur { MailUtilisateur = "test@exemple.com", Id = "16" }
        };

        _avisRepositoryMock
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Avis, bool>>>()))
            .ReturnsAsync(new List<Avis> { avis });

        _avisRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Avis>()))
            .Returns(Task.CompletedTask);

        var result = await useCase.ExecuteAsync("test@exemple.com", "Test Logement");

        Assert.IsTrue(result);
        _avisRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Avis>()), Times.Once);
    }
}