using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskBoad.Application.Abstracoes;
using TaskBoad.Infrastructure.Persistencia;
using TaskBoad.Infrastructure.Persistencia.Interceptadores;

namespace TaskBoad.Infrastructure;

public static class DependencyInjection
{
    /// <summary>Registra o banco (EF Core + Postgres) e os serviços da Infrastructure.</summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "A connection string 'TaskBoad' não foi configurada. Adicione-a nos Segredos do Usuário do projeto TaskBoad.Api.");

        services.AddSingleton<AuditoriaInterceptor>();

        services.AddDbContext<AppDbContext>((provedor, opcoes) =>
            opcoes.UseNpgsql(connectionString)
                  .AddInterceptors(provedor.GetRequiredService<AuditoriaInterceptor>()));

        services.AddScoped<IAppDbContext>(provedor => provedor.GetRequiredService<AppDbContext>());

        return services;
    }
}
