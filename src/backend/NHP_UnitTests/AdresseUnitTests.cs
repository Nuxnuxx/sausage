using Moq;
using NHP_Domain.DataAdapters;
using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.UseCases.AdresseUseCases.Create;
using NHP_Domain.UseCases.AdresseUseCases.Delete;
using NHP_Domain.UseCases.AdresseUseCases.Get;
using NHP_Domain.UseCases.AdresseUseCases.Update;

namespace NHP_UnitTests;

public class AdresseUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateAdresseUseCase()
    {
        long id = 1;
        String nomAdresse = "21 rue du dragon";
        String complementAdresse = "batiment goku";
        String ville = "capital nord";
        int codePostal = 12345;
        String pays = "Japon";
        
        Adresse adresseAvant = new Adresse{Id = id, 
            NomAdresse = nomAdresse, 
            ComplementAdresse = complementAdresse,
            Ville = ville,
            CodePostal = codePostal,
            Pays = pays
        };

        var mockAdresse = new Mock<IAdresseRepository>();
        
        mockAdresse.Setup(repo => repo.FindByConditionAsync(a => a.Id.Equals(id)))
            .ReturnsAsync((List<Adresse>)null);

        Adresse adresseFinal = new Adresse
        {
            Id = id,
            NomAdresse = nomAdresse,
            ComplementAdresse = complementAdresse,
            Ville = ville,
            CodePostal = codePostal,
            Pays = pays
        };

        mockAdresse.Setup(repo => repo.CreateAsync(adresseAvant)).ReturnsAsync(adresseFinal);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(facto => facto.AdresseRepository()).Returns(mockAdresse.Object);
        
        CreateAdresseUseCase uc = new CreateAdresseUseCase(mockFactory.Object);
        
        var adresseTest = await uc.ExecuteAsync(adresseAvant);
        
        Assert.That(adresseTest.Id, Is.EqualTo(adresseAvant.Id));
        Assert.That(adresseTest.NomAdresse, Is.EqualTo(nomAdresse));
        Assert.That(adresseTest.ComplementAdresse, Is.EqualTo(complementAdresse));
        Assert.That(adresseTest.Ville, Is.EqualTo(ville));
        Assert.That(adresseTest.CodePostal, Is.EqualTo(codePostal));
        Assert.That(adresseTest.Pays, Is.EqualTo(pays));
    }
    
    [Test]
    public async Task DeleteAdresseUseCase()
    {
        
        long id = 1;
        string nomAdresse = "21 rue du dragon";
        string complementAdresse = "batiment goku";
        string ville = "capital nord";
        int codePostal = 12345;
        string pays = "Japon";

        Adresse adresseToDelete = new Adresse
        {
            Id = id,
            NomAdresse = nomAdresse,
            ComplementAdresse = complementAdresse,
            Ville = ville,
            CodePostal = codePostal,
            Pays = pays
        };

        var mockAdresseRepo = new Mock<IAdresseRepository>();
        mockAdresseRepo.Setup(repo => repo.DeleteAsync(adresseToDelete)).Returns(Task.CompletedTask);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.AdresseRepository()).Returns(mockAdresseRepo.Object);

        var useCase = new DeleteAdresseUseCase(mockFactory.Object);

        
        bool result = await useCase.ExecuteAsync(adresseToDelete);

        
        Assert.IsTrue(result);
        mockAdresseRepo.Verify(repo => repo.DeleteAsync(adresseToDelete), Times.Once);
    }
    
    [Test]
    public async Task UpdateAdresseUseCase()
    {
        long id = 1;
        var logements = new List<Logement>();

        Adresse adresseBeforeUpdate = new Adresse
        {
            Id = id,
            NomAdresse = "Ancienne adresse",
            ComplementAdresse = "Ancien complément",
            Ville = "Ancienne ville",
            CodePostal = 11111,
            Pays = "Ancien pays",
            Logements = logements
        };

        
        Adresse adresseToUpdate = new Adresse
        {
            Id = id,
            NomAdresse = "Nouvelle adresse",
            ComplementAdresse = "Nouveau complément",
            Ville = "Nouvelle ville",
            CodePostal = 99999,
            Pays = "Nouveau pays",
            Logements = logements
        };

        var mockAdresseRepo = new Mock<IAdresseRepository>();
        mockAdresseRepo.Setup(repo => repo.UpdateAsync(adresseToUpdate)).ReturnsAsync(adresseToUpdate);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.AdresseRepository()).Returns(mockAdresseRepo.Object);
        mockFactory.Setup(f => f.SaveChangesAsync()).Returns(Task.CompletedTask);

        var useCase = new UpdateAdresseUseCase(mockFactory.Object);
        
        var updatedAdresse = await useCase.ExecuteAsync(adresseToUpdate);
        
        Assert.IsNotNull(updatedAdresse);
        Assert.That(updatedAdresse.Id, Is.EqualTo(id));
        Assert.That(updatedAdresse.NomAdresse, Is.EqualTo("Nouvelle adresse"));
        Assert.That(updatedAdresse.ComplementAdresse, Is.EqualTo("Nouveau complément"));
        Assert.That(updatedAdresse.Ville, Is.EqualTo("Nouvelle ville"));
        Assert.That(updatedAdresse.CodePostal, Is.EqualTo(99999));
        Assert.That(updatedAdresse.Pays, Is.EqualTo("Nouveau pays"));

        mockAdresseRepo.Verify(repo => repo.UpdateAsync(adresseToUpdate), Times.Once);
        mockFactory.Verify(f => f.SaveChangesAsync(), Times.Once);
    }
    
    [Test]
    public async Task GetAdresseByPrimaryKeyUseCase()
    {
        
        string nomAdresse = "21 rue du dragon";

        Adresse expectedAdresse = new Adresse
        {
            Id = 1,
            NomAdresse = nomAdresse,
            ComplementAdresse = "Bâtiment Goku",
            Ville = "Capital Nord",
            CodePostal = 12345,
            Pays = "Japon",
            Logements = new List<Logement>()
        };

        var mockAdresseRepo = new Mock<IAdresseRepository>();
        mockAdresseRepo.Setup(repo => repo.FindAsync(nomAdresse))
            .ReturnsAsync(expectedAdresse);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.AdresseRepository()).Returns(mockAdresseRepo.Object);

        var useCase = new GetAdresseByPrimaryKeyUseCase(mockFactory.Object);
        
        var result = await useCase.ExecuteAsync(nomAdresse);
        
        Assert.IsNotNull(result);
        Assert.That(result!.NomAdresse, Is.EqualTo(nomAdresse));
        Assert.That(result.Id, Is.EqualTo(expectedAdresse.Id));
        Assert.That(result.Pays, Is.EqualTo(expectedAdresse.Pays));
        Assert.That(result.Ville, Is.EqualTo(expectedAdresse.Ville));

        mockAdresseRepo.Verify(repo => repo.FindAsync(nomAdresse), Times.Once);
    }
    
    [Test]
    public async Task GetTouteLesAdressesUseCase()
    {
        var mockAdresses = new List<Adresse>
        {
            new Adresse
            {
                Id = 1,
                NomAdresse = "21 rue du dragon",
                ComplementAdresse = "Bâtiment Goku",
                Ville = "Capital Nord",
                CodePostal = 12345,
                Pays = "Japon",
                Logements = new List<Logement>()
            },
            new Adresse
            {
                Id = 2,
                NomAdresse = "99 avenue du ninja",
                ComplementAdresse = "Tour Naruto",
                Ville = "Konoha",
                CodePostal = 54321,
                Pays = "Japon",
                Logements = new List<Logement>()
            }
        };

        var mockAdresseRepo = new Mock<IAdresseRepository>();
        mockAdresseRepo.Setup(repo => repo.FindAllAsync()).ReturnsAsync(mockAdresses);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(f => f.AdresseRepository()).Returns(mockAdresseRepo.Object);

        var useCase = new GetTouteLesAdressesUseCase(mockFactory.Object);
        
        var result = await useCase.ExecuteAsync();
        
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].NomAdresse, Is.EqualTo("21 rue du dragon"));
        Assert.That(result[1].Ville, Is.EqualTo("Konoha"));

        mockAdresseRepo.Verify(repo => repo.FindAllAsync(), Times.Once);
    }
}