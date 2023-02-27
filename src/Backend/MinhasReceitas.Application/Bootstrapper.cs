using Microsoft.Extensions.DependencyInjection;
using MinhasReceitas.Application.UseCases.Usuario.Registrar;

namespace MinhasReceitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
    }
}
