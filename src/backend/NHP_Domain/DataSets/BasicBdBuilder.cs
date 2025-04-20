using NHP_Domain.DataAdapters.DataAdaptersFactory;
using NHP_Domain.Entities;
using NHP_Domain.Services;
using NHP_Domain.UseCases.AdresseUseCases.Create;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationRole.Create;
using NHP_Domain.UseCases.ApplicationUseCases.ApplicationUtilisateur.Create;
using NHP_Domain.UseCases.AvisUseCases.Create;
using NHP_Domain.UseCases.ImageUseCases.Create;
using NHP_Domain.UseCases.LogementUseCases.Create;
using NHP_Domain.UseCases.PrestationUseCases.Create;
using NHP_Domain.UseCases.RemiseUseCases.Create;
using NHP_Domain.UseCases.ReservationUseCases.Create;
using NHP_Domain.UseCases.TypeDestinationUseCases.Create;

namespace NHP_Domain.DataSets;

public class BasicBdBuilder : BdBuilder
{
    private readonly IRoleManagementService _roleManagementService;
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly List<Utilisateur> _createdUsers = new();
    private readonly List<Logement> _createdLogements = new();

    public BasicBdBuilder(IRepositoryFactory repositoryFactory, IRoleManagementService roleManagementService) 
        : base(repositoryFactory)
    {
        _roleManagementService = roleManagementService;
        _repositoryFactory = repositoryFactory;
    }

    private static readonly string PASSWORD = "Password1!";

    private static readonly TypeDestination[] _typeDestinations =
    {
        new TypeDestination { NomTypeDestination = "Mer", DescriptionTypeDestination = "Profitez des plages ensoleillées et des activités nautiques pour des vacances inoubliables." },
        new TypeDestination { NomTypeDestination = "Campagne", DescriptionTypeDestination = "Découvrez la tranquillité de la nature et les paysages verdoyants pour un séjour reposant." },
        new TypeDestination { NomTypeDestination = "Montagne", DescriptionTypeDestination = "Appréciez l'air frais, les randonnées et les sports d'hiver dans un cadre majestueux." },
        new TypeDestination { NomTypeDestination = "Ville", DescriptionTypeDestination = "Explorez les centres urbains dynamiques, leur culture, leur gastronomie et leur histoire." },
        new TypeDestination { NomTypeDestination = "Île", DescriptionTypeDestination = "Évadez-vous sur des îles paradisiaques avec des plages de sable fin et des eaux cristallines." }
    };
    
    private static readonly Adresse[] _adresses =
    {
        new Adresse
        {
            Pays = "France",
            Ville = "Soultzeren",
            CodePostal = 68140,
            NomAdresse = "1 Chemin du Ober Eck",
            ComplementAdresse = ""
        },
        new Adresse
        {
            Pays = "France",
            Ville = "Stosswihr",
            CodePostal = 68140,
            NomAdresse = "5 Chemin du Looch",
            ComplementAdresse = ""
        },
        new Adresse
        {
            Pays = "France",
            Ville = "Paris",
            CodePostal = 75001,
            NomAdresse = "15 Rue de Rivoli",
            ComplementAdresse = "Appartement 3B"
        },
        new Adresse
        {
            Pays = "France",
            Ville = "Nice",
            CodePostal = 06000,
            NomAdresse = "25 Promenade des Anglais",
            ComplementAdresse = ""
        },
        new Adresse
        {
            Pays = "France",
            Ville = "Bordeaux",
            CodePostal = 33000,
            NomAdresse = "8 Place de la Bourse",
            ComplementAdresse = ""
        }
    };
    
    private static readonly Image[] _images =
    {
        new Image
        {
            NomImage = "Soultzeren",
            UrlImage = "https://www.francethisway.com/images/places/munster.jpg",
        },
        new Image
        {
            NomImage = "Paris",
            UrlImage = "https://plus.unsplash.com/premium_photo-1661919210043-fd847a58522d",
        },
        new Image
        {
            NomImage = "Nice",
            UrlImage = "https://plus.unsplash.com/premium_photo-1688137881227-91b6322c5fe3",
        },
        new Image
        {
            NomImage = "Bordeaux",
            UrlImage = "https://images.unsplash.com/photo-1493564738392-d148cfbd6eda",
        }
    };

    private static readonly Prestation[] _prestations =
    {
        new Prestation { NomPrestation = "Randonnée" },
        new Prestation { NomPrestation = "Vélo" },
        new Prestation { NomPrestation = "Spa" },
        new Prestation { NomPrestation = "Piscine" },
        new Prestation { NomPrestation = "Petit-déjeuner" },
        new Prestation { NomPrestation = "Location de voiture" },
        new Prestation { NomPrestation = "Visite guidée" }
    };

    protected override async Task RegenerateDbAsync()
    {
        await _repositoryFactory.EnsureDeleteAsync();
        await _repositoryFactory.EnsureCreatedAsync();
    }

    protected override async Task BuildRolesAsync()
    {
        await new CreateApplicationRoleUseCase(_repositoryFactory, _roleManagementService).ExecuteAsync("Admin");
        await new CreateApplicationRoleUseCase(_repositoryFactory, _roleManagementService).ExecuteAsync("Agent");
        await new CreateApplicationRoleUseCase(_repositoryFactory, _roleManagementService).ExecuteAsync("Client");
    }

    protected override async Task BuildUsersAsync()
    {
        var utilisateurs = new[]
        {
            new Utilisateur
            {
                Id = Guid.NewGuid().ToString(),
                MailUtilisateur = "loic.cambray@etud.u-picardie.fr",
                PrenomUtilisateur = "Loïc",
                NomUtilisateur = "Cambray",
                CiviliteUtilisateur = "Homme",
                TelephoneUtilisateur = "0769999999",
                NewsLetterUtilisateur = true
            },
            new Utilisateur
            {
                Id = Guid.NewGuid().ToString(),
                MailUtilisateur = "yvone.galgena@example.com",
                PrenomUtilisateur = "Yvone",
                NomUtilisateur = "Galgena",
                CiviliteUtilisateur = "Femme",
                TelephoneUtilisateur = "0768888888",
                NewsLetterUtilisateur = true
            },
            new Utilisateur
            {
                Id = Guid.NewGuid().ToString(),
                MailUtilisateur = "sacha.ketchum@example.com",
                PrenomUtilisateur = "Sacha",
                NomUtilisateur = "Ketchum",
                CiviliteUtilisateur = "Homme",
                TelephoneUtilisateur = "0767777777",
                NewsLetterUtilisateur = false
            }
        };

        foreach (var utilisateur in utilisateurs)
        {
            var createdUtilisateur = await _repositoryFactory.UtilisateurRepository().CreateAsync(utilisateur);
            await _repositoryFactory.SaveChangesAsync();
            
            var uc = new CreateApplicationUtilisateurUseCase(_repositoryFactory);
            var role = utilisateur.MailUtilisateur == "loic.cambray@etud.u-picardie.fr" ? "Admin" : "Client";
            var createdUser = await uc.ExecuteAsync(utilisateur.MailUtilisateur, utilisateur.MailUtilisateur, PASSWORD, role, utilisateur);
            _createdUsers.Add(utilisateur);
        }
    }

    protected override async Task BuildAdressesAsync()
    {
        foreach (var adresse in _adresses)
        {
            await new CreateAdresseUseCase(_repositoryFactory).ExecuteAsync(adresse);
        }
    }

    protected override async Task BuildImageAsync()
    {
        foreach (var image in _images)
        {
            await new CreateImageUseCase(_repositoryFactory).ExecuteAsync(image);
        }
    }

    protected override async Task BuildPrestationAsync()
    {
        foreach (var prestation in _prestations)
        {
            await new CreatePrestationUseCase(_repositoryFactory).ExecuteAsync(prestation);
        }
    }

    protected override async Task BuildPrestationProposeeAsync()
    {
        var prestationsProposees = new[]
        {
            new PrestationProposee
            {
                Prestation = _prestations[0],
                PrixPrestation = 20,
            },
            new PrestationProposee
            {
                Prestation = _prestations[1],
                PrixPrestation = 15,
            },
        };

        foreach (var prestationpropose in prestationsProposees)
        {
            await new CreatePrestationProposeeUseCase(_repositoryFactory).ExecuteAsync(prestationpropose);
        }
    }

    protected override async Task BuildLogementAsync()
    {
        var logements = new[]
        {
            new Logement
            {
                NomLogement = "Hôtel du Ober Eck",
                DescriptionLogement = "Un hôtel au bord de la rivière avec vue imprenable sur les montagnes.",
                Image = _images[0],
                Adresse = _adresses[0],
                Destination = _typeDestinations[0],
                PrixLogement = 100,
            },
            new Logement
            {
                NomLogement = "Hôtel du Looch",
                DescriptionLogement = "Un hôtel dans le village avec accès direct aux sentiers de randonnée.",
                Image = _images[0],
                Adresse = _adresses[1],
                Destination = _typeDestinations[0],
                PrixLogement = 150,
            },
            new Logement
            {
                NomLogement = "Appartement Paris Centre",
                DescriptionLogement = "Appartement moderne en plein cœur de Paris, à deux pas du Louvre.",
                Image = _images[1],
                Adresse = _adresses[2],
                Destination = _typeDestinations[4],
                PrixLogement = 200,
            },
            new Logement
            {
                NomLogement = "Villa Nice Plage",
                DescriptionLogement = "Villa de luxe avec vue sur la mer Méditerranée et accès privé à la plage.",
                Image = _images[2],
                Adresse = _adresses[3],
                Destination = _typeDestinations[3],
                PrixLogement = 300,
            },
            new Logement
            {
                NomLogement = "Château Bordeaux",
                DescriptionLogement = "Château historique au cœur du vignoble bordelais, avec cave à vin.",
                Image = _images[3],
                Adresse = _adresses[4],
                Destination = _typeDestinations[3],
                PrixLogement = 250,
            }
        };

        foreach (var logement in logements)
        {
            var createdLogement = await new CreateLogementUseCase(_repositoryFactory).ExecuteAsync(logement);
            _createdLogements.Add(createdLogement);
        }
    }

    protected override async Task BuildRemiseAsync()
    {
        var remises = new[]
        {
            new Remise
            {
                CodeRemise = "REMISE1",
                NomRemise = "Remise 1",
                PourcentageRemise = 10,
            },
            new Remise
            {
                CodeRemise = "REMISE2",
                NomRemise = "Remise 2",
                PourcentageRemise = 20,
            },
        };

        foreach (var remise in remises)
        {
            await new CreateRemiseUseCase(_repositoryFactory).ExecuteAsync(remise);
        }
    }

    protected override async Task BuildReservationAsync()
    {
        if (_createdUsers.Count == 0)
            throw new InvalidOperationException("Aucun utilisateur n'a été créé. Impossible de créer des réservations.");
        
        if (_createdLogements.Count == 0)
            throw new InvalidOperationException("Aucun logement n'a été créé. Impossible de créer des réservations.");

        var reservations = new[]
        {
            new Reservation
            {
                NumeroReservation = 1,
                Utilisateur = _createdUsers[0],
                Logement = _createdLogements[0],
                PrixReservation = 150,
                ArriveeReservation = DateTime.Now.AddDays(-7),
                DepartReservation = DateTime.Now.AddDays(-5),
                ParticipantReservation = 2,
                EtatReservation = "Confirmée",
                CartePaiement = 123456789,
                StatutPaiement = "Payé"
            },
            new Reservation
            {
                NumeroReservation = 2,
                Utilisateur = _createdUsers[1],
                Logement = _createdLogements[2],
                PrixReservation = 400,
                ArriveeReservation = DateTime.Now.AddDays(-14),
                DepartReservation = DateTime.Now.AddDays(-12),
                ParticipantReservation = 4,
                EtatReservation = "Confirmée",
                CartePaiement = 987654321,
                StatutPaiement = "Payé"
            },
            new Reservation
            {
                NumeroReservation = 3,
                Utilisateur = _createdUsers[2],
                Logement = _createdLogements[3],
                PrixReservation = 900,
                ArriveeReservation = DateTime.Now.AddDays(-21),
                DepartReservation = DateTime.Now.AddDays(-18),
                ParticipantReservation = 2,
                EtatReservation = "Confirmée",
                CartePaiement = 456789123,
                StatutPaiement = "Payé"
            }
        };

        foreach (var reservation in reservations)
        {
            await new CreateReservationUseCase(_repositoryFactory).ExecuteAsync(reservation);
        }
    }

    protected override async Task BuildAvisAsync()
    {
        if (_createdUsers.Count == 0)
            throw new InvalidOperationException("Aucun utilisateur n'a été créé. Impossible de créer des avis.");
        
        if (_createdLogements.Count == 0)
            throw new InvalidOperationException("Aucun logement n'a été créé. Impossible de créer des avis.");

        var avis = new[]
        {
            new Avis
            {
                Logement = _createdLogements[0],
                Utilisateur = _createdUsers[0],
                NoteAvis = 4,
                TexteAvis = "Super hôtel ! Très bon séjour, je recommande vivement. Le personnel est très accueillant et la vue est magnifique."
            },
            new Avis
            {
                Logement = _createdLogements[2],
                Utilisateur = _createdUsers[1],
                NoteAvis = 5,
                TexteAvis = "Appartement parfaitement situé, très propre et bien équipé. Idéal pour visiter Paris."
            },
            new Avis
            {
                Logement = _createdLogements[3],
                Utilisateur = _createdUsers[2],
                NoteAvis = 3,
                TexteAvis = "Belle villa mais un peu bruyante à cause de la route. La vue sur la mer est cependant exceptionnelle."
            }
        };

        foreach (var unAvis in avis)
        {
            await new CreateAvisUseCase(_repositoryFactory).ExecuteAsync(unAvis);
        }
    }

    protected override async Task BuildTypeDestinationAsync()
    {
        foreach (var typeDestination in _typeDestinations)
        {
            await new CreateTypeDestinationUseCase(_repositoryFactory).ExecuteAsync(typeDestination);
        }
    }
}