# API Nexthorizon Plus

Ce projet est l'API backend de l'application Nexthorizon Plus, une plateforme de réservation de logements.

## Prérequis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) (version 8.0 ou supérieure)
- [Visual Studio](https://visualstudio.microsoft.com/fr/) ou [Visual Studio Code](https://code.visualstudio.com/)

## Configuration

### 1. Configuration de la base de données

Créez un fichier `.env` à la racine du projet avec les informations suivantes :

```env
DB_SERVER=localhost
DB_PORT=3306
DB_USER=root
DB_PASSWORD=votre_mot_de_passe
DB_NAME=nexthorizonplusdb
```

Remplacez les valeurs par vos propres paramètres de connexion MySQL.

### 2. Création de la base de données

Exécutez les commandes suivantes dans MySQL :

```sql
CREATE DATABASE nexthorizonplusdb;
```

## Lancement de l'API

### 1. Installation des dépendances

```bash
dotnet restore
```

### 2. Construction du projet

```bash
dotnet build
```

### 3. Lancement de l'API

```bash
dotnet run
```

L'API sera accessible à l'adresse : `http://localhost:5185`

## Fonctionnalités

- Gestion des utilisateurs
- Gestion des logements
- Gestion des réservations
- Gestion des avis
- Système d'authentification

## Structure du projet

- `Controllers/` : Contient les contrôleurs de l'API
- `Configuration/` : Contient les configurations de l'application
- `Models/` : Contient les modèles de données
- `Services/` : Contient les services métier

## Tests

Pour lancer les tests :

```bash
dotnet test
```

## Documentation

La documentation Swagger est disponible à l'adresse : `https://localhost:5185/swagger`

## Sécurité

- L'API utilise JWT pour l'authentification
- Les mots de passe sont hachés avant d'être stockés
- Les routes sont protégées par des rôles