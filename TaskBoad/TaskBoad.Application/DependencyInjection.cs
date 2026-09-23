using Microsoft.Extensions.DependencyInjection;
using TaskBoad.Application.Perfis.ObterMeuPerfil;

namespace TaskBoad.Application;

public static class DependencyInjection
{
    /// <summary>Registra os casos de uso (handlers). Cada novo handler entra aqui.</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ObterMeuPerfilHandler>();
        return services;
    }
}
