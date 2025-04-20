using System;
using Microsoft.EntityFrameworkCore;

namespace NHP_RestApi.Configuration;

public static class DatabaseConfiguration
{
    public static string GetConnectionString()
    {
        var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") 
            ?? throw new InvalidOperationException("DB_SERVER is not set");
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT") 
            ?? throw new InvalidOperationException("DB_PORT is not set");
        var dbUser = Environment.GetEnvironmentVariable("DB_USER") 
            ?? throw new InvalidOperationException("DB_USER is not set");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") 
            ?? throw new InvalidOperationException("DB_NAME is not set");

        return $"Server={dbServer};" +
               $"Port={dbPort};" +
               $"User={dbUser};" +
               $"Password={dbPassword};" +
               $"Database={dbName};";
    }

    public static void ConfigureDbContext(DbContextOptionsBuilder options, string connectionString)
    {
        options.UseMySQL(connectionString);
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
} 