using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhasReceitas.Domain.Extension;
using System.Reflection;

namespace MinhasReceitas.Infrastructure;

public static class Bootstrapper
{
    public static void AddRepositorio(this IServiceCollection services, IConfiguration connectionManager)
    {
        AddFluentMigrator(services, connectionManager);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration connectionManager)
    {
        services.AddFluentMigratorCore().ConfigureRunner(c => 
        c.AddMySql5()
        .WithGlobalConnectionString(connectionManager.GetConexaoCompleta()).ScanIn(Assembly.Load("MinhasReceitas.Infrastructure")).For.All()
        );
    }
}
