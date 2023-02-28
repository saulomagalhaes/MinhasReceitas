using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhasReceitas.Application.Servicos.Criptografia;
using MinhasReceitas.Application.Servicos.Token;
using MinhasReceitas.Application.UseCases.Usuario.Registrar;

namespace MinhasReceitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarUseCases(services);
        AdicionarChaveAdicionalSenha(services, configuration);
        AdicionarTokenJwt(services, configuration);
    }

    private static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
    }

    private static void AdicionarChaveAdicionalSenha(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuracoes:ChaveAdicionalSenha");

        services.AddScoped(options => new EncriptadorDeSenha(section.Value));
    }

    private static void AdicionarTokenJwt(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoDeVida = configuration.GetRequiredSection("Configuracoes:TempoDeVidaToken");
        var sectionKey = configuration.GetRequiredSection("Configuracoes:ChaveToken");

        services.AddScoped(options => new TokenController(int.Parse(sectionTempoDeVida.Value), sectionKey.Value));
    }
}
