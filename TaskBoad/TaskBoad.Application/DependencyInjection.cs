using Microsoft.Extensions.DependencyInjection;
using TaskBoad.Application.Perfis.ObterMeuPerfil;

namespace TaskBoad.Application;

public static class DependencyInjection
{
    // Registra os casos de uso (handlers). Cada novo handler entra aqui.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ObterMeuPerfilHandler>();
        return services;
    }
}
