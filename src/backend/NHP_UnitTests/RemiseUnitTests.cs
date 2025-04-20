using System.Linq.Expressions;
using Moq;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Exceptions.RemiseExceptions;
using NHP_Domain.UseCases.RemiseUseCases.Create;
using NHP_Domain.UseCases.RemiseUseCases.Delete;
using NHP_Domain.UseCases.RemiseUseCases.Get;
using NHP_Domain.UseCases.RemiseUseCases.Update;

namespace NHP_UnitTests;

public class RemiseUnitTest
{
    private Mock<IRepositoryFactory> _repositoryFactoryMock;
    private Mock<IRemiseRepository> _remiseRepositoryMock;

    [SetUp]
    public void Setup()
    {
        // Initialisation des mocks pour chaque test
        _remiseRepositoryMock = new Mock<IRemiseRepository>();
        _repositoryFactoryMock = new Mock<IRepositoryFactory>();
        _repositoryFactoryMock.Setup(rf => rf.RemiseRepository()).Returns(_remiseRepositoryMock.Object);
    }

    // Test pour vérifier la création d'une remise valide
    [Test]
    public async Task CreateRemiseUseCase_ValidRemise()
    {
        var useCase = new CreateRemiseUseCase(_repositoryFactoryMock.Object);
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1", PourcentageRemise = 10 };
        _remiseRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Remise>())).ReturnsAsync(remise);
        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Remise, bool>>>())).ReturnsAsync(new List<Remise>());
        
        var result = await useCase.ExecuteAsync(remise);
        
        Assert.IsNotNull(result);
        Assert.That(result.NomRemise, Is.EqualTo("Test"));
        _remiseRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Remise>()), Times.Once);
    }

    // Test pour vérifier le comportement lorsque la remise existe déjà
    [Test]
    public void CreateRemiseUseCase_RemiseExists()
    {
        var useCase = new CreateRemiseUseCase(_repositoryFactoryMock.Object);
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1", PourcentageRemise = 10 };
        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Remise, bool>>>())).ReturnsAsync(new List<Remise> { remise });

        Assert.ThrowsAsync<AlreadyExistException>(async () => await useCase.ExecuteAsync(remise));
    }

    // Test pour vérifier le comportement lorsque le code de remise est trop court
    [Test]
    public void CreateRemiseUseCase_CodeRemiseTooShort()
    {
        var useCase = new CreateRemiseUseCase(_repositoryFactoryMock.Object);
        var remise = new Remise { NomRemise = "Test", CodeRemise = "COD", PourcentageRemise = 10 };

        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Remise, bool>>>()))
            .ReturnsAsync(new List<Remise>());

        Assert.ThrowsAsync<TooShortException>(async () => await useCase.ExecuteAsync(remise));
    }
    
    // Test pour vérifier le comportement lorsque le nom de la remise est trop court
    [Test]
    public void CreateRemiseUseCase_NomRemiseTooShort()
    {
        var remise = new Remise { NomRemise = "Te", CodeRemise = "CODE1", PourcentageRemise = 10 };
        
        _repositoryFactoryMock.Setup(rf => rf.RemiseRepository()).Returns(_remiseRepositoryMock.Object);

        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Remise, bool>>>()))
            .ReturnsAsync(new List<Remise>());

        var useCase = new CreateRemiseUseCase(_repositoryFactoryMock.Object);

        Assert.ThrowsAsync<TooShortException>(async () => await useCase.ExecuteAsync(remise));
    }

    // Test pour vérifier le comportement lorsque le pourcentage de la remise est trop élevé
    [Test]
    public void CreateRemiseUseCase_PercentageLimit()
    {
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1", PourcentageRemise = 101 };
        
        _repositoryFactoryMock.Setup(rf => rf.RemiseRepository()).Returns(_remiseRepositoryMock.Object);

        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Remise, bool>>>()))
            .ReturnsAsync(new List<Remise>());

        var useCase = new CreateRemiseUseCase(_repositoryFactoryMock.Object);

        Assert.ThrowsAsync<PercentLimitException>(async () => await useCase.ExecuteAsync(remise));
    }
    
    // Test pour vérifier la récupération de toutes les remises
    [Test]
    public async Task GetAllRemisesUseCase_ReturnAllRemises()
    {
        
        var useCase = new GetAllRemisesUseCase(_repositoryFactoryMock.Object);
        var remises = new List<Remise> { new Remise { NomRemise = "Test1" }, new Remise { NomRemise = "Test2" } };
        _remiseRepositoryMock.Setup(repo => repo.FindAllAsync()).ReturnsAsync(remises);
        
        var result = await useCase.ExecuteAsync();
        
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        _remiseRepositoryMock.Verify(repo => repo.FindAllAsync(), Times.Once);
    }

    // Test pour vérifier la récupération d'une remise par son code
    [Test]
    public async Task GetRemiseByCodeUseCase_ReturnRemiseWithCode()
    {
        var useCase = new GetRemiseByCodeUseCase(_repositoryFactoryMock.Object);
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1" };
        _remiseRepositoryMock.Setup(repo => repo.FindAsync("CODE1")).ReturnsAsync(remise);
        
        var result = await useCase.ExecuteAsync("CODE1");
        
        Assert.IsNotNull(result);
        Assert.That(result.NomRemise, Is.EqualTo("Test"));
        _remiseRepositoryMock.Verify(repo => repo.FindAsync("CODE1"), Times.Once);
    }

    // Test pour vérifier le comportement lorsque le code de remise n'existe pas
    [Test]
    public async Task GetRemiseByCodeUseCase_ReturnCodeDoesNotExist()
    {
        var useCase = new GetRemiseByCodeUseCase(_repositoryFactoryMock.Object);
        _remiseRepositoryMock.Setup(repo => repo.FindAsync("CODE2")).ReturnsAsync((Remise)null);
        
        var result = await useCase.ExecuteAsync("CODE2");
        
        Assert.IsNull(result);
        _remiseRepositoryMock.Verify(repo => repo.FindAsync("CODE2"), Times.Once);
    }

    // Test pour vérifier la mise à jour d'une remise
    [Test]
    public async Task UpdateRemiseUseCase_ValidRemise()
    {
        var useCase = new UpdateRemiseUseCase(_repositoryFactoryMock.Object);
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1", PourcentageRemise = 10 };
        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Remise, bool>>>())).ReturnsAsync(new List<Remise>());
        
        var result = await useCase.ExecuteAsync(remise);
        
        Assert.IsNotNull(result);
        Assert.That(result.NomRemise, Is.EqualTo("Test"));
        _remiseRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Remise>()), Times.Once);
    }

    // Test pour vérifier le comportement lors de la mise à jour lorsque le code remise existe déjà
    [Test]
    public void UpdateRemiseUseCase_CodeRemiseExists()
    {
        var useCase = new UpdateRemiseUseCase(_repositoryFactoryMock.Object);
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1", PourcentageRemise = 10 };
        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Remise, bool>>>())).ReturnsAsync(new List<Remise> { remise });

        Assert.ThrowsAsync<AlreadyExistException>(async () => await useCase.ExecuteAsync(remise));
    }

    // Test pour vérifier le comportement lors de la mise à jour lorsque le code de remise est trop court
    [Test]
    public void UpdateRemiseUseCase_CodeRemiseTooShort()
    {
        var remise = new Remise { NomRemise = "Test", CodeRemise = "COD", PourcentageRemise = 10 };
        
        _repositoryFactoryMock.Setup(rf => rf.RemiseRepository()).Returns(_remiseRepositoryMock.Object);

        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Remise, bool>>>()))
            .ReturnsAsync(new List<Remise>());

        var useCase = new UpdateRemiseUseCase(_repositoryFactoryMock.Object);

        Assert.ThrowsAsync<TooShortException>(async () => await useCase.ExecuteAsync(remise));
    }
    
    // Test pour vérifier le comportement lors de la mise à jour lorsque le nom de la remise est trop court
    [Test]
    public void UpdateRemiseUseCase_NomRemiseTooShort()
    {
        var remise = new Remise { NomRemise = "Te", CodeRemise = "CODE1", PourcentageRemise = 10 };
        
        _repositoryFactoryMock.Setup(rf => rf.RemiseRepository()).Returns(_remiseRepositoryMock.Object);

        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Remise, bool>>>()))
            .ReturnsAsync(new List<Remise>());

        var useCase = new UpdateRemiseUseCase(_repositoryFactoryMock.Object);

        Assert.ThrowsAsync<TooShortException>(async () => await useCase.ExecuteAsync(remise));
    }

    // Test pour vérifier le comportement lors de la mise à jour lorsque le pourcentage de la remise est trop faible
    [Test]
    public void UpdateRemiseUseCase_PercentageLimit()
    {
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1", PourcentageRemise = -12 };
        
        _repositoryFactoryMock.Setup(rf => rf.RemiseRepository()).Returns(_remiseRepositoryMock.Object);

        _remiseRepositoryMock.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Remise, bool>>>()))
            .ReturnsAsync(new List<Remise>());

        var useCase = new UpdateRemiseUseCase(_repositoryFactoryMock.Object);

        Assert.ThrowsAsync<PercentLimitException>(async () => await useCase.ExecuteAsync(remise));
    }
    
    // Test pour vérifier la suppression d'une remise
    [Test]
    public async Task DeleteRemiseUseCase_CodeExists()
    {
        var useCase = new DeleteRemiseUseCase(_repositoryFactoryMock.Object);
        var remise = new Remise { NomRemise = "Test", CodeRemise = "CODE1" };
        _remiseRepositoryMock.Setup(repo => repo.FindAsync("CODE1")).ReturnsAsync(remise);
        
        var result = await useCase.ExecuteAsync("CODE1");
        
        Assert.IsTrue(result);
        _remiseRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Remise>()), Times.Once);
    }

    // Test pour vérifier le comportement lors de la suppression d'une remise qui n'existe pas
    [Test]
    public async Task DeleteRemiseUseCase_CodeDoesNotExist()
    {
        var useCase = new DeleteRemiseUseCase(_repositoryFactoryMock.Object);
        _remiseRepositoryMock.Setup(repo => repo.FindAsync("CODE2")).ReturnsAsync((Remise)null);
        
        var result = await useCase.ExecuteAsync("CODE2");
        
        Assert.IsFalse(result);
        _remiseRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Remise>()), Times.Never);
    }
}