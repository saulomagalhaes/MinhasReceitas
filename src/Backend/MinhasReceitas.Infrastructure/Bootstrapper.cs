using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhasReceitas.Domain.Extension;
using MinhasReceitas.Domain.Repositorios;
using MinhasReceitas.Infrastructure.AcessoRepositorio;
using MinhasReceitas.Infrastructure.AcessoRepositorio.Repositorio;
using System.Reflection;

namespace MinhasReceitas.Infrastructure;

public static class Bootstrapper
{
    public static void AddRepositorio(this IServiceCollection services, IConfiguration connectionManager)
    {
        AddFluentMigrator(services, connectionManager);
        AddRepositorios(services);
        AddUnidadeDeTrabalho(services);
        AdicionarContexto(services, connectionManager);
    }

    private static void AdicionarContexto(IServiceCollection services, IConfiguration connectionManager)
    {
        var connectionString = connectionManager.GetConexaoCompleta();

        services.AddDbContext<MinhasReceitasContext>(dbContextoOpcoes =>
            dbContextoOpcoes.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );
    }

    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
    }

    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioWriteOnlyRepositorio, UsuarioRepositorio>()
            .AddScoped<IUsuarioReadOnlyRepositorio, UsuarioRepositorio>();
    }
    private static void AddFluentMigrator(IServiceCollection services, IConfiguration connectionManager)
    {
        services.AddFluentMigratorCore().ConfigureRunner(c => 
        c.AddMySql5()
        .WithGlobalConnectionString(connectionManager.GetConexaoCompleta()).ScanIn(Assembly.Load("MinhasReceitas.Infrastructure")).For.All()
        );
    }
}
